using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private MainMenu _mainMenu;

    [SerializeField] private PauseMenu _pauseMenu;

    [SerializeField] private Camera _dummyCamera;

    public Events.EventFadeComplete OnMainMenuFadeComplete;

    void Start()
    {
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
        _mainMenu.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
    }

    void Update()
    {
        if (GameManager.Instance.CurrentGameState != GameManager.GameState.PREGAME)
        {
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
        _pauseMenu.gameObject.SetActive(currentState == GameManager.GameState.PAUSE);
    }

    void HandleMainMenuFadeComplete(bool fadeOut)
    {
        OnMainMenuFadeComplete.Invoke(fadeOut);
    }

    public void SetDummyCameraActive(bool active)
    {
        _dummyCamera.enabled = active;
        //_dummyCamera.gameObject.SetActive (true);
    }     
}
