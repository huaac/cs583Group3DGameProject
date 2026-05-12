using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathHandler : MonoBehaviour
{
    public void Die()
    {
        PlayerInfo.numFails++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}