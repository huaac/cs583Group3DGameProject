using UnityEngine;

public class ResetPrefs : MonoBehaviour
{
    void Awake()
    {
        PlayerPrefs.DeleteAll();

        Debug.Log("PlayerPrefs cleared");
    }
}