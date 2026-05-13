using UnityEngine;

public class RevealButton : MonoBehaviour
{

    public GameObject button1;
    public GameObject button2;
    public PlayerMovement playerScript;

    private float timer = 0f;
    private bool isRevealed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button2.SetActive(false);
        isRevealed = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(playerScript.isMoving == true && isRevealed == false)
        {
            timer = 0f;
        } 

        if(timer >= 10f && isRevealed == false)
        {
            isRevealed = true;
            button1.SetActive(false);
            button2.SetActive(true);
        }
    }
}
