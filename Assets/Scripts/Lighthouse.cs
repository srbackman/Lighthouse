using UnityEngine;
using UnityEngine.Events;

public class Lighthouse : MonoBehaviour
{
    [SerializeField] private int _sourceCount = 4;

    [SerializeField] private UnityEvent _fullPowerEvent;

    private int _activePowerSources = 0;

    public void PowerAdd()
    {
        _activePowerSources++;

        if (_activePowerSources == 1) GameManager.Instance.InvokeActivateGuideCables();
        else if (_activePowerSources == _sourceCount) _fullPowerEvent?.Invoke();
    }
}
