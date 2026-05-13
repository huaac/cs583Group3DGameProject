using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class ChangeScenes : MonoBehaviour
{
    public void startButtonClick()
    {
        PlayerInfo.firstLoad = true;
        SceneManager.LoadScene("Level1");
    }
    public static void endGame()
    {
        PlayerInfo.firstLoad = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("Finish");
    }
    public static void restartGame()
    {
        PlayerInfo.firstLoad = true;
        PlayerInfo.numFails = 0;
        PlayerInfo.level2trapTriggered = false;
        PlayerInfo.dialogTriggered = new Dictionary<string, bool>();
        SceneManager.LoadScene("MainMenu");
    } 
}
