using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public delegate void ActivateGuideCables();
    public event ActivateGuideCables OnActivateGuideCables;

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
}
