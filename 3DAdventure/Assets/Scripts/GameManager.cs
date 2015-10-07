using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    
    public enum GameState
    {
        Loading,
        Running,
        Ending
    }
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
        if (_instance && _instance != this)
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
                End();
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
           }
        }



        if(collectables.Count == 0)
        {
            currentState = GameState.Ending;
        }
    }

    void End()
    {
        currentLevel++;
        if(currentLevel >= levels.Count)
        {
            Application.LoadLevel(0);
            currentState = GameState.Loading;
            levelStart = false;
            currentLevel = 0;
            Debug.Log("Game is Done");
        }
        else
        {
            Application.LoadLevel(levels[currentLevel]);
            currentState = GameState.Loading;
            StartCoroutine(prepLevel());
        }
    }



        
}
