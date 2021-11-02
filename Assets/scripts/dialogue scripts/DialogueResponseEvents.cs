using UnityEngine;
using System;

public class DialogueResponseEvents : MonoBehaviour
{
    [SerializeField]
    private DialogueObject dialogueObject;

    [SerializeField]
    private ResponseEvent[] events;

    public DialogueObject DialogueObject => dialogueObject;
    
    public ResponseEvent[] Events => events;

    public void OnValidate()
    {
        //conditions to check if we need to return
        if(dialogueObject == null)
        {
            return;
        }

        //conditions to check if we need to return
        if(dialogueObject.Responses == null)
        {
            return;
        }

        //conditions to check if we need to return
        if(events != null && events.Length == dialogueObject.Responses.Length)
        {
            return;
        }

        //if events is null, make a new array with the size
        if(events == null)
        {
            events = new ResponseEvent[dialogueObject.Responses.Length];
        }

        //resize the array
        else
        {
            Array.Resize(ref events, dialogueObject.Responses.Length);
        }

        //loop through all dialogue object responses
        for(int i=0; i < dialogueObject.Responses.Length; i++)
        {
            Response response = dialogueObject.Responses[i];

            if(events[i] != null)  
            {
                events[i].name = response.ResponseText;
            }      

            events[i] = new ResponseEvent{name=response.ResponseText};
        }
    }
}
