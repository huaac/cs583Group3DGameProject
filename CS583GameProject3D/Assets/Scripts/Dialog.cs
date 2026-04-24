using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public GameObject dialogText;
    private string curText = "";
    private float secsPerChar = 0.1f;
    private float timeDialogDisappears = 3f;
    private Coroutine dialogPlaying;
    private bool playing = false;
    private TextMeshProUGUI textMesh;
    private Image background;

    [TextArea]
    public string startingGameText;
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
        StartCoroutine(UpdateText(startingGameText));
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
