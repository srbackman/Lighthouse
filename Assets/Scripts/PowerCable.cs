using UnityEngine;

public class PowerCable : MonoBehaviour
{
    private bool _powerOn = false;

    void Start()
    {
        GameManager.Instance.OnActivateGuideCables += ActivateGuideLight;
    }


    private void ActivateGuideLight()
    {
        
    }

    public void ActivatePowerLight()
    {
        
    }
}
