using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    
    public Camera PlaneCam;
    public Camera islandCam;
    public Health planeHealth;
    TowerController towerController;
    Text winnerText;

    GameObject inGameUI;
    GameObject postGameUI;

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
    public Transform PlaneTargetTransform {get; private set;}

    bool planeWins = false;

   
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

	void Start () 
    {
        currentState = GameState.Running;
        //DontDestroyOnLoad(this);
        PlaneTargetTransform = GameObject.Find("PLANE_TARGET").transform;
        planeHealth = GameObject.Find("PlayerOne").GetComponent<Health>();
        PlaneCam = GameObject.Find("Camera1").GetComponent<Camera>();
        islandCam = GameObject.Find("Camera").GetComponent<Camera>();
        winnerText = GameObject.Find("WinnerText").GetComponent<Text>();

        towerController = GameObject.Find("Towers").GetComponent<TowerController>();

        inGameUI = GameObject.Find("InGame");
        postGameUI = GameObject.Find("PostGame");
        postGameUI.SetActive(false);

	}
	
	// Update is called once per frame


    void Update()
    {
        switch(currentState)
        {
            case GameState.Running:
                RunLevel();
                break;
            case GameState.Ending:
                StartCoroutine(End());
                break;
            case GameState.Delay:
                    break;
        }
    }

    void RunLevel()
    {
        //check Plane Health
        if(!planeHealth.IsAlive())
        {
            planeWins = false;
            currentState = GameState.Ending;
        }
        else if (towerController.AllTowersDisabled())
        {
            planeWins = true;
            planeHealth.GetComponent<PlayerBombControl>().enabled = false;
            currentState = GameState.Ending;
        }
        //Check tower status
    }

    IEnumerator End()
    {
        inGameUI.SetActive(false);
        postGameUI.SetActive(true);
        currentState = GameState.Delay;
        Camera activeCam = null;
        if(planeWins)
        {
            activeCam = PlaneCam;
            winnerText.text = "Player One Wins";
        }
        else 
        {
            towerController.DisableCamera();
            activeCam = islandCam;
            islandCam.transform.GetChild(0).gameObject.SetActive(false);

            winnerText.text = "Player Two Wins";
        }
     
        if(activeCam != null)
        {
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
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true ;



    }

    public GameObject GetShellPrefab()
    {
        return TurretShellPrefab;
    }

        
}
