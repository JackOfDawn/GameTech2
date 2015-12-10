using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
    public Text player2Score;
    public Text player1Score;
    public Camera p1Cam;
    public Camera p2Cam;

    public GameObject TurretShellPrefab;


    public enum GameState
    {
        Null,
        Loading,
        Running,
        Ending,
        Delay
    }
    public float endOfLevelDelay = 3;
    public GameState currentState;
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }
	// Use this for initialization
    public List<string> levels;
    int currentLevel = 0;
    bool levelStart = false;

    public List<Collectable> collectables;
    public int[] score;

    public Transform PlaneTargetTransform {get; private set;}
   
    void Awake()
    {
        //if (GameManager.Instance != this)
            //Destroy(this.gameObject);
        score = new int [2];
        score[0] = score[1] = 0;
    }

	void Start () 
    {
        currentState = GameState.Null;
        //DontDestroyOnLoad(this);
        PlaneTargetTransform = GameObject.Find("PLANE_TARGET").transform;

	}
	
	// Update is called once per frame


    IEnumerator prepLevel()
    {
        levelStart = false;
        yield return new WaitForSeconds(1);

        foreach (var collectable in FindObjectsOfType<Collectable>())
        {
            collectables.Add(collectable); 
        }

        levelStart = true;
        score[0] = score[1] = 0;
    }
    void Update()
    {
        switch(currentState)
        {
            case GameState.Loading:
                if (levelStart)
                    currentState = GameState.Running;
                break;
            case GameState.Running:
                RunLevel();
                break;
            case GameState.Ending:
                StartCoroutine(End());
                break;
            case GameState.Delay:
                    {
                        if (Input.GetKeyDown(KeyCode.Backspace))
                        {
                            StopAllCoroutines();
                            Application.LoadLevel(0);
                            
                        }
                    }
                    break;
        }
    }

    void RunLevel()
    {
        for (int i = collectables.Count - 1; i >= 0; i--)
        {
           if(!collectables[i])
           {
               collectables.RemoveAt(i);
               Debug.Log("Collectables Remaining: " + collectables.Count);
           }
        }



        if(collectables.Count == 0)
        {
            currentState = GameState.Ending;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            currentState = GameState.Ending;
        }
    }

    IEnumerator End()
    {
        yield return null;
        
        currentState = GameState.Delay;
        Camera activeCam = null;
        if(score[0] > score[1])
        {
            p2Cam.gameObject.SetActive(false);
            activeCam = p1Cam;
            player1Score.text = "Player 1";
        }
        else if (score[0] < score [1])
        {
            p1Cam.gameObject.SetActive(false);
            activeCam = p2Cam;
            player1Score.text = "Player 2";
        }
     
        if(activeCam != null)
        {
            player2Score.text = "Wins";
            Rect rect = activeCam.rect;
            for (int i = 0; i < 100; i++)
            {
                
                rect.x = Mathf.Lerp(rect.x, 0, Time.deltaTime * 5);
                rect.y = Mathf.Lerp(rect.y, 0, Time.deltaTime * 5);
                rect.width = Mathf.Lerp(rect.width, 1, Time.deltaTime * 5);
                rect.height = Mathf.Lerp(rect.height, 1, Time.deltaTime * 5);
                activeCam.rect = rect;
                yield return null;
            }

            rect = activeCam.rect;
            rect.x = 0;
            rect.y = 0;
            rect.width = 1;
            rect.height = 1;
            activeCam.rect = rect;
        }
        else
        {
            player2Score.text = "Draw";
            player1Score.text = "Draw";
        }

        
    

        yield return new WaitForSeconds(endOfLevelDelay);

    }

    public void collected(int player)
    {
        score[player] += 1;

        player1Score.text = "Score: " + score[0] + " rings";
        player2Score.text = "Score: " + score[1] + " rings";

    }

    public GameObject GetShellPrefab()
    {
        return TurretShellPrefab;
    }

        
}
