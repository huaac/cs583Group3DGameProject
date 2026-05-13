using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScenes : MonoBehaviour
{
    public void startButtonClick()
    {
        SceneManager.LoadScene("Level1");
    }
    public static void endGame()
    {
        SceneManager.LoadScene("Finish");
    }
}
