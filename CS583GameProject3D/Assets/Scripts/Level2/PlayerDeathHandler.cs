using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathHandler : MonoBehaviour
{
    public void Die()
    {
        Time.timeScale = 1f;
        PlayerInfo.numFails++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}