using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private Camera _dummyCamera;
    [SerializeField] private PauseMenu _pauseMenu;

    void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    void Update()
    {
        if (GameManager.Instance.CurrentGameState != GameManager.GameState.PREGAME)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.CurrentGameState == GameManager.GameState.RUNNING)
            {
                GameManager.Instance.PauseGame();
            }
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //_mainMenu.FadeOut();
            GameManager.Instance.StartGame();
        }        
    }

    void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        //Debug.Log("[UIManager] HandleGameStateChanged");
        if (previousState == GameManager.GameState.RUNNING && currentState == GameManager.GameState.PAUSE)
            GamePaused();

        if (previousState == GameManager.GameState.PAUSE && currentState == GameManager.GameState.RUNNING)
            GameRunning();
    }

    public void SetDummyCameraActive(bool active)
    {
        _dummyCamera.enabled = active;
        //_dummyCamera.gameObject.SetActive (true);
    }     

    public void SetPauseMenuActive(bool active)
    {
        _pauseMenu.gameObject.SetActive(active);
    }

    void GamePaused()
    {
    	SetDummyCameraActive(true);
        SetPauseMenuActive(true);
    }

    void GameRunning()
    {
        SetDummyCameraActive(false);
        SetPauseMenuActive(false);
    }
}
