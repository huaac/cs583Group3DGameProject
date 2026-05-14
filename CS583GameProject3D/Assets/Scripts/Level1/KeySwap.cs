using UnityEngine;
using UnityEngine.SceneManagement;

public class KeySwap : MonoBehaviour
{
    private Transform player;

    public bool teleportDisabled = false;

    [Header("Teleport Points")]
    public Transform teleportPoint1;
    public Transform teleportPoint2;

    private bool atPoint1 = true;

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
            if (atPoint1)
            {
                transform.position = teleportPoint2.position;
            }
            else
            {
                transform.position = teleportPoint1.position;
            }

            atPoint1 = !atPoint1;
        }
        else
        {
            Debug.Log("Key collected!");
            gameObject.SetActive(false);
            PlayerInfo.firstLoad = true;
            SceneManager.LoadScene("Level2");
        }
    }
}