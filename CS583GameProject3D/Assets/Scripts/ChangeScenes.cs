using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class ChangeScenes : MonoBehaviour
{
    //Loads next scene
    public void startButtonClick()
    {
        PlayerInfo.firstLoad = true;
        SceneManager.LoadScene("Level1");
    }

    //loads finished scene
    public static void endGame()
    {
        PlayerInfo.firstLoad = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("Finish");
    }

    //restarts the level
    public static void restartGame()
    {
        PlayerInfo.firstLoad = true;
        PlayerInfo.numFails = 0;
        PlayerInfo.level2trapTriggered = false;
        PlayerInfo.dialogTriggered = new Dictionary<string, bool>();
        SceneManager.LoadScene("MainMenu");
    } 

    //quits the game
    public static void QuitGame()
    {
        Application.Quit();
    } 
}
