using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Image _fadeTarget;
    [SerializeField] private float _fadeOutDuration = 2f;

    void Start()
    {
        _continueButton.onClick.AddListener(() => Close());
        //_continueButton.onClick.AddListener(() => StartCoroutine(FadeOut()));

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Close()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _fadeTarget.gameObject.SetActive(false);
        GameManager.Instance.PlayerInMenu = false;
    }

    private IEnumerator FadeOut()
    {
        float timer = 0f;
        Color color = _fadeTarget.color;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        while (timer < _fadeOutDuration)
        {
            _fadeTarget.color = new Color(color.r, color.g, color.b, Mathf.Lerp(1f, 0f, timer / _fadeOutDuration));

            yield return null;

            _fadeOutDuration += Time.deltaTime;
        }

        _fadeTarget.gameObject.SetActive(false);
        GameManager.Instance.PlayerInMenu = false;
    }
}
