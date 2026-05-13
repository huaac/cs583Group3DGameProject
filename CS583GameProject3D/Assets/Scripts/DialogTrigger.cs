using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;
    public string dialogName;
    [TextArea]
    public string triggerMessage;
    void Start()
    {
        if (PlayerInfo.dialogTriggered.ContainsKey(dialogName))
        {
            if (PlayerInfo.dialogTriggered[dialogName] == true)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            PlayerInfo.dialogTriggered.Add(dialogName, false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") &  PlayerInfo.dialogTriggered[dialogName] == false)
        {
            PlayerInfo.dialogTriggered[dialogName] = true;
            StartCoroutine(dialog.UpdateText(triggerMessage));
        }
    }
}
