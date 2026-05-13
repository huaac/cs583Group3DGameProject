using UnityEngine;

public class BlockSwitch : MonoBehaviour
{
    public KeySwap keySwap;

    private Vector3 startPos;
    private bool activated = false;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // If block moved enough
        if (!activated &&
            Vector3.Distance(transform.position, startPos) > 1f)
        {
            activated = true;

            // Disable teleporting
            keySwap.teleportDisabled = true;

            Debug.Log("Teleport disabled!");
        }
    }
}
