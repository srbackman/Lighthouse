using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private float _masterVolume = 0.8f;
    public float MasterVolume { get {return _masterVolume;} }

    private float _musicVolume = 0.7f;
    public float MusicVolume { get {return _musicVolume;} }

    private float _effectsVolume = 0.8f;
    public float EffectsVolume { get {return _effectsVolume;} }

    private float _lookSensitivity = 90f;
    public float LookSensitivity { get {return _lookSensitivity;} }

    public delegate void ActivateGuideCables();
    public event ActivateGuideCables OnActivateGuideCables;

    public delegate void LookSensitivityChange(float value);
    public event LookSensitivityChange OnLookSensitivityChange;

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
}
