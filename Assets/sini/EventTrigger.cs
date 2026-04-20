using UnityEngine;

public class EventTrigger : MonoBehaviour
{

        // Drag the Door Pivot object into this slot in the Inspector
        public DoorOpen targetDoor;

        // This could be called by a trigger zone, a button press, etc.
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Event Happened! Opening Door...");
                targetDoor.OpenDoor();
            }
        }
    }
