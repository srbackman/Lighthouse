using UnityEngine;

public class WinCube : MonoBehaviour
{
    private bool _oneShot = false;

    void OnTriggerEnter(Collider other)
    {
        if (_oneShot) return;

        _oneShot = true;
        GameManager.Instance.InvokeYouWin();
    }
}
