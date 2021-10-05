using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]

//class for DialogueObjects, which inherit from ScriptableObjects
public class DialogueObject : ScriptableObject
{
    //reference to dialogue text array
    [SerializeField] 
    [TextArea] private string[] dialogue;

    //reference to an array of responses
    [SerializeField]
    private Response[] responses;

    //getter functions
    public string[] Dialogue => dialogue;

    public Response[] Responses => responses;

    //check if there are any responses for a given dialogue object
    public bool HasResponses => Responses != null && Responses.Length > 0;
}
