using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    #region Sliders
    private float _masterVolume = 0f;
    public float MasterVolume { get {return _masterVolume;} }

    private float _musicVolume = -10f;
    public float MusicVolume { get {return _musicVolume;} }

    private float _effectsVolume = -20f;
    public float EffectsVolume { get {return _effectsVolume;} }
    
    private float _lookSensitivity = 90f;
    public float LookSensitivity { get {return _lookSensitivity;} }
    #endregion

    #region Events
    public delegate void ActivateGuideCables();
    public event ActivateGuideCables OnActivateGuideCables;

    public delegate void LookSensitivityChange(float value);
    public event LookSensitivityChange OnLookSensitivityChange;

    public delegate void OpenPauseMenu();
    public event OpenPauseMenu OnOpenPauseMenu;
    #endregion

    public bool PlayerInMenu = true;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    void Start()
    {
        
    }

    public void InvokeActivateGuideCables() { OnActivateGuideCables?.Invoke(); }

    public void InvokeLookSensitivityChange(float value) { _lookSensitivity = value; OnLookSensitivityChange?.Invoke(value); }

    public void InvokeOpenPauseMenu() { OnOpenPauseMenu?.Invoke(); }
}
