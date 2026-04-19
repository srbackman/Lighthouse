using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _quitButton;
    [Header("Settings")]
    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _effectsVolumeSlider;
    [SerializeField] private Slider _lookSensitivitySlider;
    [Space]
    [SerializeField] private AudioMixer _audioMixer;


    void Awake()
    {
        _resumeButton.onClick.AddListener(() => ClosePauseMenu());
        _quitButton.onClick.AddListener(() => QuitGame());

        _masterVolumeSlider.onValueChanged.AddListener((value) => ChangeMasterVolume(value));
        _musicVolumeSlider.onValueChanged.AddListener((value) => ChangeMusicVolume(value));
        _effectsVolumeSlider.onValueChanged.AddListener((value) => ChangeEffectVolume(value));

        _lookSensitivitySlider.onValueChanged.AddListener((value) => ChangeLookSensitivity(value));

        StartCoroutine(Setup());
    }

    private IEnumerator Setup()
    {
        yield return new WaitUntil(() => GameManager.Instance != null);

        _masterVolumeSlider.value = GameManager.Instance.MasterVolume;
        _musicVolumeSlider.value = GameManager.Instance.MusicVolume;
        _effectsVolumeSlider.value = GameManager.Instance.EffectsVolume;

        _lookSensitivitySlider.value = GameManager.Instance.LookSensitivity;

        GameManager.Instance.OnOpenPauseMenu += OpenPauseMenu;

        gameObject.SetActive(false);
    }

    private void OpenPauseMenu()
    {
        GameManager.Instance.PlayerInMenu = true;
        gameObject.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void ClosePauseMenu()
    {
        GameManager.Instance.PlayerInMenu = false;
        gameObject.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void QuitGame() { Application.Quit(); }

    private void ChangeMasterVolume(float value) { _audioMixer.SetFloat("MasterVolume", value); }

    private void ChangeMusicVolume(float value) { _audioMixer.SetFloat("MusicVolume", value); }

    private void ChangeEffectVolume(float value) { _audioMixer.SetFloat("EffectVolume", value); }

    private void ChangeLookSensitivity(float value) { GameManager.Instance.InvokeLookSensitivityChange(value); }
}
