using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private MainMenu _mainMenu;

    [SerializeField] private Camera _dummyCamera;

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

    public void SetDummyCameraActive(bool active)
    {
        _dummyCamera.enabled = active;
        //_dummyCamera.gameObject.SetActive (true);
    }     
}
