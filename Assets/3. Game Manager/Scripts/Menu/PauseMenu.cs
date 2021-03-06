using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button ResumeButton;
    [SerializeField] private Button RestartButton;
    [SerializeField] private Button QuitButton;

    void Start()
    {
        ResumeButton.onClick.AddListener(HandleResumeClicked);
        RestartButton.onClick.AddListener(HandleRestartClicked);
        QuitButton.onClick.AddListener(HandleQuitCliked);
    }

    void HandleResumeClicked()
    {
        GameManager.Instance.ToggleMenu();
    }

    void HandleRestartClicked()
    {
        GameManager.Instance.RestartGame();
    }

    void HandleQuitCliked()
    {
        GameManager.Instance.QuitGame();
    }
}
