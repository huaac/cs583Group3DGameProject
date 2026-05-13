using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class FallingTrap : MonoBehaviour
{
    public Rigidbody trapRb;

    public float fallDelay = 0.5f;

    public string trapID;

    private bool triggered = false;

    void Start()
    {
        if (PlayerInfo.level2trapTriggered)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            PlayerInfo.level2trapTriggered = true;

            StartCoroutine(Fall());
        }
    }

    IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);

        trapRb.isKinematic = false;
        trapRb.useGravity = true;

        yield return new WaitForSeconds(2f);

        gameObject.SetActive(false);
    }
}