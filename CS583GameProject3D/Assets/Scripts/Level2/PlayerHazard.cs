using UnityEngine;
using System.Collections;

public class PlayerHazard : MonoBehaviour
{
    private PlayerDeathHandler deathHandler;

    [Header("Sounds")]
    public AudioSource spikeSound;
    public AudioSource fallSound;

    [Header("Player Renderers")]
    private Renderer[] renderers;

    [Header("Player Components")]
    private Rigidbody rb;

    // Drag your movement script here in Inspector
    public MonoBehaviour movementScript;

    private bool isDying;

    void Start()
    {
        deathHandler = GetComponent<PlayerDeathHandler>();

        // Get all renderers in player + children
        renderers = GetComponentsInChildren<Renderer>();

        // Get Rigidbody
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDying) return;

        if (other.CompareTag("Spike"))
        {
            StartCoroutine(SpikeDeath());
        }
        else if (other.CompareTag("KillZone"))
        {
            StartCoroutine(FallDeath());
        }
    }

    IEnumerator SpikeDeath()
    {
        isDying = true;

        // Freeze player
        FreezePlayer();

        // Turn player red
        MakePlayerRed();

        // Play sound
        spikeSound.Play();

        // Wait for sound
        yield return new WaitForSeconds(spikeSound.clip.length);

        // Restart scene
        deathHandler.Die();
    }

        IEnumerator FallDeath()
    {
        isDying = true;

        fallSound.Play();

        yield return new WaitForSeconds(fallSound.clip.length);

        deathHandler.Die();
    }

    void FreezePlayer()
    {
        // Stop movement velocity
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Freeze physics movement
        rb.constraints = RigidbodyConstraints.FreezeAll;

        // Disable movement script
        if (movementScript != null)
        {
            movementScript.enabled = false;
        }
    }

    void MakePlayerRed()
    {
        foreach (Renderer renderer in renderers)
        {
            foreach (Material mat in renderer.materials)
            {
                mat.color = Color.red;
            }
        }
    }
}