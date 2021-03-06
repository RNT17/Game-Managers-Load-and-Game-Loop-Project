using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    // Load Level
    // Unload Level
    // keep track of the game state
    // generate other persistent systems

    public enum GameState 
    {
        PREGAME,
        RUNNING,
        PAUSE,
    }
    
    public GameObject[] systemPrefabs;
    public Events.EventGameState OnGameStateChanged; // Register events

    private List<GameObject> _instancedSystemPrefabs;
    List<AsyncOperation> _loadOperations;
    private string _currentLevelName = string.Empty;

    GameState _currentGameState = GameState.PREGAME;

    public GameState CurrentGameState
    {
        get { return _currentGameState; }
        set { _currentGameState = value; }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        _loadOperations = new List<AsyncOperation>();
        _instancedSystemPrefabs = new List<GameObject>();

        InstantiateSystemPrefabs();

        UIManager.Instance.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
    }

    void Update()
    {
        if (_currentGameState == GameState.PREGAME)
            return; 

        if (Input.GetKeyDown(KeyCode.Escape))
            ToggleMenu();
    }

    void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (_loadOperations.Contains(ao))
        {
            _loadOperations.Remove(ao);

            if (_loadOperations.Count == 0)
            {
                UpdateState(GameState.RUNNING);
            }
        }

        Debug.Log("Load Complete");
    }

    void HandleMainMenuFadeComplete(bool fadeOut)
    {
        if (!fadeOut)
            UnloadLevel(_currentLevelName);
    }

    void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unload Complete");
    }

    void UpdateState(GameState state)
    {
        GameState previousGameState = _currentGameState;
        _currentGameState = state;

        switch (_currentGameState)
        {
            case GameState.PREGAME:
                Time.timeScale = 1.0f;
                break;
            case GameState.RUNNING:
                Time.timeScale = 1.0f;
                break;
            case GameState.PAUSE:
                Time.timeScale = 0.0f;
                break;
            default:
                break;
        }

        // dispath message
        OnGameStateChanged.Invoke(_currentGameState, previousGameState);
    }

    void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;
        for (int i = 0; i < systemPrefabs.Length; ++i)
        {
            prefabInstance = Instantiate(systemPrefabs[i]);
            _instancedSystemPrefabs.Add(prefabInstance);
        }
    }

    public void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to load level " + levelName);
            return;
        }
        
        ao.completed += OnLoadOperationComplete;
        _loadOperations.Add(ao);

        _currentLevelName = levelName;
    }

    public void UnloadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to Unload level " + levelName);
            return;
        }
        ao.completed += OnUnloadOperationComplete;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        for (int i = 0; i < _instancedSystemPrefabs.Count; ++i)
        {
            Destroy(_instancedSystemPrefabs[i]);
        }
        _instancedSystemPrefabs.Clear();
    }

    public void StartGame()
    {
        LoadLevel("Main");    
    }

    public void ToggleMenu()
    {
        UpdateState(_currentGameState == GameState.RUNNING ? GameState.PAUSE : GameState.RUNNING);
    }

    public void RestartGame()
    {
        UpdateState(GameManager.GameState.PREGAME);
    }

    public void QuitGame()
    {
        print("Quit");
        Application.Quit();
    }
}


