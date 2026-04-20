using UnityEngine;
using System.Collections;

public class DoorOpen : MonoBehaviour
{
    public bool isOpen = false;
    public float openAngle = 90f;
    public float smoothTime = 2f;

    private Quaternion _closedRotation;
    private Quaternion _openRotation;

    void Awake()
    {
        // Store the starting rotation as "closed"
        _closedRotation = transform.localRotation;
        // Calculate the "open" rotation based on the Y axis
        _openRotation = _closedRotation * Quaternion.Euler(0, openAngle, 0);
    }

    // This is the function other scripts will call
    public void OpenDoor()
    {
        if (!isOpen)
        {
            StopAllCoroutines();
            StartCoroutine(RotateDoor(_openRotation));
            isOpen = true;
        }
    }

    IEnumerator RotateDoor(Quaternion targetRotation)
    {
        float elapsed = 0;
        Quaternion startingRot = transform.localRotation;

        while (elapsed < smoothTime)
        {
            // Smoothly interpolate between current and target
            transform.localRotation = Quaternion.Slerp(startingRot, targetRotation, elapsed / smoothTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localRotation = targetRotation;
    }
}

