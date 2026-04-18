using UnityEngine;
using UnityEngine.Events;

public class SliderPuzzle : MonoBehaviour
{
    [SerializeField] private Transform _winPosition;
    [SerializeField] private Transform _targetPart;
    [SerializeField] private float _activationThreshold;
    [SerializeField] private UnityEvent _unityEvent;

    private bool _puzzleDone = false;
    [HideInInspector] public bool PuzzleDone;
    
    void Update()
    {
        if (_puzzleDone) return;

        if (Vector3.Distance(_winPosition.position, _targetPart.position) <= _activationThreshold)
        {
            _puzzleDone = true;
            _unityEvent.Invoke();
        }
    }
}
