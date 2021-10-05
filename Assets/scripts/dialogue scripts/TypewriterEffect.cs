using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//typewritereffect class, which inherits froom monobehavior
//used to have the "typewriter" style writing of dialogue/responses
public class TypewriterEffect : MonoBehaviour
{
    //reference to dictate the writing speed of the dialogue/response when it is "written"
    [SerializeField]
    private float writingSpeed;

    //function to run
    public Coroutine Run(string textToType, TMP_Text textLabel)
    {
        return StartCoroutine(TypeText(textToType, textLabel));
    }

    //function to show the text being typed
    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        //text should initially be empty
        textLabel.text = string.Empty;

        float timeWriting = 0;
        int characterIndex = 0;

        //while there is still text to show
        while(characterIndex < textToType.Length)
        {
            //calculate what characters to show
            timeWriting += Time.deltaTime * writingSpeed;
            characterIndex = Mathf.FloorToInt(timeWriting);
            characterIndex = Mathf.Clamp(characterIndex,0,textToType.Length);

            //text that should appear at a given time
            textLabel.text = textToType.Substring(0, characterIndex);

            yield return null;

        }

        textLabel.text = textToType;
    }
}
