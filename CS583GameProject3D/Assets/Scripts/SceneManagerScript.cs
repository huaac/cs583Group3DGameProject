using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class SceneManagerScript : MonoBehaviour
{
    // loads scene lvl4
   public void LoadScene4()
   {
       PlayerInfo.firstLoad = true;
       SceneManager.LoadScene("Level4");
   }
}