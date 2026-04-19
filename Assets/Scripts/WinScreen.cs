using System.Collections;
using TMPro;
using UnityEngine;

public class WinScreen : MonoBehaviour
{   
    public TMP_Text _youWinText;
    public TMP_Text _goodbyeText;
    public TMP_Text _countDownText;

    void Awake()
    {
        StartCoroutine(Setup());
    }

    private IEnumerator Setup()
    {
        yield return new WaitUntil(() => GameManager.Instance != null);

        GameManager.Instance.OnYouWin += StartWinScreen;
        gameObject.SetActive(false);
    }

    private void StartWinScreen()
    {print("here");
        gameObject.SetActive(true);
        StartCoroutine(YouWin());
    }

    private IEnumerator YouWin()
    {
        yield return new WaitForSeconds(5f);

        _youWinText.gameObject.SetActive(false);
        _goodbyeText.gameObject.SetActive(true);
        _countDownText.gameObject.SetActive(true);
        _countDownText.text = "3";

        float timer = 4f;

        while (timer > 0f)
        {
            _countDownText.text = ((int)timer).ToString();
            yield return null;
            timer -= Time.deltaTime;
        }

        Application.Quit();
    }
}
