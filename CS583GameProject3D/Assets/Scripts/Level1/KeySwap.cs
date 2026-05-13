using UnityEngine;
using UnityEngine.SceneManagement;

public class KeySwap : MonoBehaviour
{
    private Transform player;

    public bool teleportDisabled = false;

    [Header("Teleport Settings")]
    public float radius = 5f;
    public int maxAttempts = 10;
    public LayerMask obstacleMask;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (!teleportDisabled)
        {
            Vector3 newPos = GetSafeRandomPosition();

            transform.position = newPos;
        }
        else
        {
            Debug.Log("Key collected!");
            gameObject.SetActive(false);
            PlayerInfo.firstLoad = true;
            SceneManager.LoadScene("Level2");
        }
    }

    Vector3 GetSafeRandomPosition()
    {
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector2 rand = Random.insideUnitCircle * radius;

            Vector3 candidate = new Vector3(
                player.position.x + rand.x,
                transform.position.y,
                player.position.z + rand.y
            );

            // check if space is free
            Collider[] hits = Physics.OverlapSphere(candidate, 0.5f, obstacleMask);

            if (hits.Length == 0)
            {
                return candidate;
            }
        }

        // fallback if all fail
        return transform.position;
    }
}