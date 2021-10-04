using TMPro;
using UnityEngine;
using System.Collections;

public class DialogueUI : MonoBehaviour
{

    [SerializeField]
    private TMP_Text textLabel;

    [SerializeField]
    private DialogueObject testDialogue;

    [SerializeField]
    private GameObject dialogueBox;

    private TypewriterEffect typeWriterEffect;

    private void Start()
    {
        CloseDialogueBox();
        typeWriterEffect = GetComponent<TypewriterEffect>();
        ShowDialogue(testDialogue);
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        foreach(string dialogue in dialogueObject.Dialogue)
        {
            yield return typeWriterEffect.Run(dialogue, textLabel);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        CloseDialogueBox();
    }

    private void CloseDialogueBox()
    {
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
