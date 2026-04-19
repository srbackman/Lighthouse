using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _quitButton;


    void Awake()
    {
        _resumeButton.onClick.AddListener(() => ClosePauseMenu());
        _quitButton.onClick.AddListener(() => QuitGame());
    }

    private void ClosePauseMenu() { gameObject.SetActive(false); }

    private void QuitGame() { Application.Quit(); }
}
