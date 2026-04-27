using UnityEngine;
using System.Collections;

public class PlayerHazard : MonoBehaviour
{
    private PlayerDeathHandler deathHandler;

    public AudioSource spikeSound;
    public AudioSource fallSound;

    private bool isDying;

    void Start()
    {
        deathHandler = GetComponent<PlayerDeathHandler>();
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

        spikeSound.Play();
        yield return new WaitForSeconds(0.05f);

        deathHandler.Die();
    }

    IEnumerator FallDeath()
    {
        isDying = true;

        fallSound.Play();
        yield return new WaitForSeconds(fallSound.clip.length);

        deathHandler.Die();
    }
}