using UnityEngine;

public class GuideLight : MonoBehaviour
{
    [SerializeField] private GameObject _meshObject;
    [SerializeField] private Light _pointLight;

    [HideInInspector] public float Timer = 0f;
    private Renderer _selfRenderer;

    void Awake()
    {
        _selfRenderer = _meshObject.GetComponent<Renderer>();
    }

    public void Setup(Color color, float strength, float startTime)
    {
        if (!_selfRenderer) _selfRenderer = _meshObject.GetComponent<Renderer>();

        _selfRenderer.material.color = color;
        _selfRenderer.material.SetVector("_EmissionColor", color);
        _pointLight.color = color;
        _pointLight.intensity = strength;
        Timer = startTime;
    }

    public void UpdateLight(float value)
    {
        _pointLight.intensity = value;
    }
}
