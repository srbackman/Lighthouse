using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum PipeType
{
    Turn,
    Straight
}

public enum PipeOrientationType
{
    Up,
    Left,
    Down,
    Right
}

public class ConnectionPuzzle : MonoBehaviour
{
    [SerializeField] private Vector2 _startPosition;
    [SerializeField] private GameObject _startObject;
    [SerializeField] private Vector2 _endPosition;
    [SerializeField] private GameObject _endObject;
    [Space]
    [SerializeField] private List<List<Pipe>> _pipes;
    [Space]
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
    private List<Vector2> _currentCompletePipe = new List<Vector2>();

    void Start() { _pointLight.color = _active ? _onColor : _offColor; }

    public void Activate() { _active = true; _pointLight.color = _onColor; _coverPanel.SetActive(false); }

    public void PipePressed(int row, int column, PipeOrientationType pipeOrientation)
    {
        if (_puzzleDone || !_active) return;

        if (Pather()) _unityEvent?.Invoke();
    }

    private bool Pather()
    {
        List<int> markForRemoval = new();
        bool isRemoving = false;

        foreach (Vector2 pipe in _currentCompletePipe)
        {
            
        }

        return false;
    }

    private void RandomizeOrientations()
    {
        
    }
}
