using UnityEngine;

public class HallwayTrigger : MonoBehaviour
{
    public EndlessHallway hallwayManager;

    private bool triggered = false;

    //if player hits the trigger, clone the 2nd to last hallway
    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            hallwayManager.SpawnNextSection();
        }
    }
}