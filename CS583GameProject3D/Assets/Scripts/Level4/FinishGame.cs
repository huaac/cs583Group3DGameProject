using UnityEngine;

public class FinishGame : MonoBehaviour
{

    /*
        Triggers the end scene once the player has completed level 4
        Params: the GameObject collided with the end trigger
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ChangeScenes.endGame();
        }
    }
}
