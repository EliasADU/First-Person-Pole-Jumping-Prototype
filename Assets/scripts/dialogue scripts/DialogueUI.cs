using TMPro;
using UnityEngine;
using System.Collections;

//class for the DialogueUI

public class DialogueUI : MonoBehaviour
{
    //reference to the label of the text
    [SerializeField]
    private TMP_Text textLabel;

    //reference to the dialogue
    [SerializeField]
    private DialogueObject testDialogue;

    //reference to the dialogue box
    [SerializeField]
    private GameObject dialogueBox;

    //variable to manage the typewritter effect used when printing text
    private TypewriterEffect typeWriterEffect;

    //variable to manage the responses
    private ResponseHandler responseHandler;

    //start function
    private void Start()
    {
        //close any pre-existing dialogue
        CloseDialogueBox();

        //set the typeWriterEffect and responseHandler
        typeWriterEffect = GetComponent<TypewriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();

        //show the dialogue box we want to show
        ShowDialogue(testDialogue);
    }

    //implementation of ShowDialogue() function
    public void ShowDialogue(DialogueObject dialogueObject)
    {
        //make the dialogue box active
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    //function that iterates through the number of "dialogues"
    //will show dialogue and responses
    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        for(int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];

            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue;

            //if we encounter a response, we break and show the response
            if(i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses)
            {
                break;
            }

            yield return null;

            //otherwise, the user can move to the next dialogue by pressing space
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        //show the responses at the end
        if(dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }

        //no responses, so close the box
        else
        {
            CloseDialogueBox();
        }
    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        typeWriterEffect.Run(dialogue, textLabel);

        while(typeWriterEffect.IsRunning)
        {
            yield return null;

            if(Input.GetKeyDown(KeyCode.Space))
            {{
                typeWriterEffect.Stop();
            }}
        }
    }

    //function that closes the dialogue box and text from the user's screen
    private void CloseDialogueBox()
    {
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
