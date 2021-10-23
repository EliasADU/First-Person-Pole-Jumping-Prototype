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

    private readonly Dictionary<HashSet<char>, float> punctuations = new Dictionary<HashSet<char>, float>()
    {
        {new HashSet<char>(){'.', '!', '?'}, 0.5f},
        {new HashSet<char>(){',', ';', ':'}, 0.25f},
    };

    public bool IsRunning{get; private set;}

    private Coroutine typingCoroutine;

    //function to run
    public void Run(string textToType, TMP_Text textLabel)
    {
        typingCoroutine = StartCoroutine(TypeText(textToType, textLabel));
    }

    public void Stop()
    {
        StopCoroutine(typingCoroutine);
        IsRunning = false;
    }

    //function to show the text being typed
    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        IsRunning = true;

        //text should initially be empty
        textLabel.text = string.Empty;

        float timeWriting = 0;
        int characterIndex = 0;

        //while there is still text to show
        while(characterIndex < textToType.Length)
        {
            int lastCharIndex = characterIndex;

            //calculate what characters to show
            timeWriting += Time.deltaTime * writingSpeed;
            characterIndex = Mathf.FloorToInt(timeWriting);
            characterIndex = Mathf.Clamp(characterIndex,0,textToType.Length);

            for(int i=lastCharIndex; i<characterIndex; i++)
            {
                bool isLast = i >= textToType.Length -1;

                //text that should appear at a given time
                textLabel.text = textToType.Substring(0, i+1);

                //check that it is punctuation, not at the end, and the following character is not punctuation
                if(IsPunctuation(textToType[i], out float waitTime) && !isLast && !IsPunctuation(textToType[i+1], out _))
                {
                    yield return new WaitForSeconds(waitTime);
                }

            }
            yield return null;

        }
        IsRunning = false;
    }

    //used to determine if a given character is a form of punctuation
    private bool IsPunctuation(char character, out float waitTime)
    {
        foreach(KeyValuePair<HashSet<char>, float> punctuationCategory in punctuations)
        {
            if(punctuationCategory.Key.Contains(character))
            {
                waitTime = punctuationCategory.Value;
                return true;
            }
        }
        waitTime = default;
        return false;
    }
}
