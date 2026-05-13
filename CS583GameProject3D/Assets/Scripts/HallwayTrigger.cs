using UnityEngine;

public class HallwayTrigger : MonoBehaviour
{
    public EndlessHallway hallwayManager;

    private bool triggered = false;

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