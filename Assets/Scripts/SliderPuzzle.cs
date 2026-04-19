using UnityEngine;
using UnityEngine.Events;

public class SliderPuzzle : MonoBehaviour
{
    [SerializeField] private Transform _winPosition;
    [SerializeField] private Transform _targetPart;
    [SerializeField] private float _activationThreshold = 0.225f;
    [SerializeField] private UnityEvent _unityEvent;
    [SerializeField] private bool _active = false;
    [Space]
    [SerializeField] private Light _pointLight;
    [SerializeField] private Color _doneColor;
    [SerializeField] private Color _onColor;
    [SerializeField] private Color _offColor;
    [Space]
    [SerializeField] private GameObject _coverPanel;

    private bool _puzzleDone = false;

    void Start()
    {
        _pointLight.color = _offColor;

        if (_active)
        {
            _pointLight.color = _onColor;
            _coverPanel.SetActive(false);
        }
    }

    public void Activate() { _active = true; _pointLight.color = _onColor; _coverPanel.SetActive(false); }

    private void Update()
    {
        if (_puzzleDone || !_active) return;

        if (Vector3.Distance(_winPosition.position, _targetPart.position) <= _activationThreshold)
        {
            _puzzleDone = true;
            _unityEvent.Invoke();
            _pointLight.color = _doneColor;
        }
    }
}
