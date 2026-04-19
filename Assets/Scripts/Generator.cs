using UnityEngine;
using UnityEngine.Events;

public class Generator : MonoBehaviour
{
    [SerializeField] private Light _pointLight;
    [Space]
    [SerializeField] private Color _onColor;
    [SerializeField] private Color _offColor;
    [Space]
    [SerializeField] private UnityEvent unityEvent;

    private bool _on = false;
    private Renderer _selfRenderer;

    void Start()
    {
        _selfRenderer = GetComponent<Renderer>();
        _selfRenderer.material.color = _offColor;
    }

    public void TurnOn()
    {
        if (_on) return;

        _on = true;
        _pointLight.gameObject.SetActive(true);
        _selfRenderer.material.color = _onColor;
        unityEvent?.Invoke();
    }
}
