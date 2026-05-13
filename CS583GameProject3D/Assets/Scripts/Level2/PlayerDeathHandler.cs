using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerDeathHandler : MonoBehaviour
{
    public void Die()
    {
        StartCoroutine(RestartScene());
    }

    IEnumerator RestartScene()
    {
        Time.timeScale = 1f;

        yield return new WaitForSeconds(0.2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}