using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

//response handler class, which inherits from monobehavior
public class ResponseHandler : MonoBehaviour
{
    //reference to the response box
    [SerializeField]
    private RectTransform responseBox;

    //reference to the response button template
    [SerializeField]
    private RectTransform responseButtonTemplate;

    //reference to the response container
    [SerializeField]
    private RectTransform responseContainer;

    private ResponseEvent[] responseEvents;

    //dialogue UI object
    private DialogueUI dialogueUI;

    //list to keep track of the responses, which are shown on the button 
    List<GameObject> tempResponseButtons = new List<GameObject>();

    //start function
    private void Start()
    {
        //initialize dialogueUI
        dialogueUI = GetComponent<DialogueUI>();
    }

    //function to set responseEvents to user parameter
    public void AddResponseEvents(ResponseEvent[] responseEvents)
    {
        this.responseEvents = responseEvents;
    }

    //function that shows the responses on the button in the top right corner
    public void ShowResponses(Response[] responses)
    {
        //variable to keep track of the box height
        float responseBoxHeight = 0;

        //iterate through all responses
        for(int i=0; i < responses.Length; i++)
        {
            //current response and index
            Response response = responses[i];
            int responseIndex = i;

            //instantiate
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            //make the button visible
            responseButton.gameObject.SetActive(true);
            //show text and let the user click
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
            responseButton.GetComponent<Button>().onClick.AddListener(()=> OnPickedResponse(response, responseIndex));

            tempResponseButtons.Add(responseButton);

            //update button size
            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }

        //resize the button and show everything
        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.gameObject.SetActive(true);
    }

    //function for when a button is clicked by the user
    private void OnPickedResponse(Response response, int responseIndex)
    {
        //after the button is clicked, you destroy it
        responseBox.gameObject.SetActive(false);

        foreach (GameObject button in tempResponseButtons)
        {
            Destroy(button);
        }

        tempResponseButtons.Clear();

        //check for in bounds index
        if(responseEvents != null && responseIndex <= responseEvents.Length)
        {
            responseEvents[responseIndex].OnPickedResponse?.Invoke();
        }

        //deallocate
        responseEvents = null;

        //now, show the dialogue instead
        if(response.DialogueObject)
        {
            dialogueUI.ShowDialogue(response.DialogueObject);
        }

        //close box: fix bug
        else
        {
            dialogueUI.CloseDialogueBox();
        }
    }
}
