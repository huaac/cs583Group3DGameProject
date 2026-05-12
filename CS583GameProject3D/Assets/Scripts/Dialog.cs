using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public GameObject dialogText;
    private string curText = "";
    private float secsPerChar = 0.05f;
    private float timeDialogDisappears = 3f;
    private Coroutine dialogPlaying;
    private bool playing = false;
    private TextMeshProUGUI textMesh;
    private Image background;

    [TextArea]
    public string startingGameText;

    [TextArea]
    public string fail10Text;
    [TextArea]
    public string fail20Text;
    [TextArea]
    public string fail50Text;
    [TextArea]
    public string fail100Text;
    void Start()
    {
        if (textMesh == null)
        {
            textMesh = dialogText.GetComponent<TextMeshProUGUI>();
        }
        if (background == null)
        {
            background = dialogText.transform.parent.GetComponent<Image>();
        }
        if (PlayerInfo.firstLoad)
        {
            StartCoroutine(UpdateText(startingGameText));
            PlayerInfo.firstLoad = false;
        }
        else if (PlayerInfo.numFails == 10)
        {
            StartCoroutine(UpdateText(fail10Text));
        }
        else if (PlayerInfo.numFails == 20)
        {
            StartCoroutine(UpdateText(fail20Text));
        }
        else if (PlayerInfo.numFails == 50)
        {
            StartCoroutine(UpdateText(fail50Text));
        }
        else if (PlayerInfo.numFails == 100)
        {
            StartCoroutine(UpdateText(fail100Text));
        }
    }
    IEnumerator PlayDialog()
    {
        playing = true;
        foreach (char c in curText)
        {
            textMesh.text += c;
            textMesh.ForceMeshUpdate();
            if (textMesh.textInfo.lineCount > 1)
            {
                textMesh.text = textMesh.text.Substring(1);
                textMesh.ForceMeshUpdate();
            }
            yield return new WaitForSeconds(secsPerChar);
        }
        playing = false;
        yield return new WaitForSeconds(timeDialogDisappears);
        textMesh.text = "";
        background.enabled = false;
    }
    public IEnumerator UpdateText(string text)
    {
        curText = text;
        if (dialogPlaying != null)
        {
            StopCoroutine(dialogPlaying);
            if (playing)
            {
                textMesh.text += "—";
            if (textMesh.textInfo.lineCount > 1)
            {
                textMesh.text = textMesh.text.Substring(1);
                textMesh.ForceMeshUpdate();
            }
                yield return new WaitForSeconds(secsPerChar);
            }
            textMesh.text = "";
        }
        
        background.enabled = true;
        dialogPlaying = StartCoroutine(PlayDialog());
    }
    
}
