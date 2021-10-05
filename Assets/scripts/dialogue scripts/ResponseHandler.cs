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

    //function that shows the responses on the button in the top right corner
    public void ShowResponses(Response[] responses)
    {
        //variable to keep track of the box height
        float responseBoxHeight = 0;

        //iterate through all responses
        foreach(Response response in responses)
        {
            //instantiate
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            //make the button visible
            responseButton.gameObject.SetActive(true);
            //show text and let the user click
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
            responseButton.GetComponent<Button>().onClick.AddListener(()=> OnPickedResponse(response));

            tempResponseButtons.Add(responseButton);

            //update button size
            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }

        //resize the button and show everything
        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.gameObject.SetActive(true);
    }

    //function for when a button is clicked by the user
    private void OnPickedResponse(Response response)
    {
        //after the button is clicked, you destroy it
        responseBox.gameObject.SetActive(false);

        foreach (GameObject button in tempResponseButtons)
        {
            Destroy(button);
        }

        tempResponseButtons.Clear();

        //now, show the dialogue instead
        dialogueUI.ShowDialogue(response.DialogueObject);
    }
}
