using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScenes : MonoBehaviour
{
    public void mainMenuButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void startButtonClick()
    {
        SceneManager.LoadScene("Level1");
    }
}
