using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathHandler : MonoBehaviour
{
    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}