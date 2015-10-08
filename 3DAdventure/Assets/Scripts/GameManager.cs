using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    
    public enum GameState
    {
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
   
    void Awake()
    {
        if (GameManager.Instance != this)
            Destroy(this.gameObject);
    }

	void Start () 
    {
        
        DontDestroyOnLoad(this);
        currentState = GameState.Loading;
        StartCoroutine(prepLevel());

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
    }

    IEnumerator End()
    {
        currentState = GameState.Delay;
        yield return new WaitForSeconds(endOfLevelDelay);
        currentLevel++;
        if(currentLevel >= levels.Count)
        {
            Application.LoadLevel(0);
            currentState = GameState.Loading;
            levelStart = false;
            currentLevel = 0;
            Debug.Log("Game is Done");
            Destroy(this.gameObject);
        }
        else
        {
            Application.LoadLevel(levels[currentLevel]);
            currentState = GameState.Loading;
            StartCoroutine(prepLevel());
        }
    }




        
}
