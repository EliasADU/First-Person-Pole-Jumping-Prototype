using UnityEngine;

//Class to Manage Responses from the Player

[System.Serializable]
public class Response 
{
    //text content
    [SerializeField]
    private string responseText;

    //reference to a dialogue object
    [SerializeField]
    private DialogueObject dialogueObject;

    //getter functions for the text and dialogue object
    public string ResponseText => responseText;

    public DialogueObject DialogueObject => dialogueObject;
}
