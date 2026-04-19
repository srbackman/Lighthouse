using UnityEngine;
using UnityEngine.Events;

public class SliderPuzzle : MonoBehaviour
{
    [SerializeField] private Transform _winPosition;
    [SerializeField] private Transform _targetPart;
    [SerializeField] private float _activationThreshold = 0.225f;
    [SerializeField] private UnityEvent _unityEvent;
    [SerializeField] private bool _active = false;
    [SerializeField] private GameObject _onVisuals;

    private bool _puzzleDone = false;
    
    public void Activate() { _active = true; _onVisuals.SetActive(true); }

    private void Update()
    {
        if (_puzzleDone || !_active) return;

        if (Vector3.Distance(_winPosition.position, _targetPart.position) <= _activationThreshold)
        {
            _puzzleDone = true;
            _unityEvent.Invoke();
            print("done!");
        }
    }
}
