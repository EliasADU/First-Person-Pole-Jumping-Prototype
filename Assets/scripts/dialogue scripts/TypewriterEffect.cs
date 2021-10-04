using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField]
    private float writingSpeed;

    public Coroutine Run(string textToType, TMP_Text textLabel)
    {
        return StartCoroutine(TypeText(textToType, textLabel));
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        textLabel.text = string.Empty;

        float timeWriting = 0;
        int characterIndex = 0;

        while(characterIndex < textToType.Length)
        {
            timeWriting += Time.deltaTime * writingSpeed;
            characterIndex = Mathf.FloorToInt(timeWriting);
            characterIndex = Mathf.Clamp(characterIndex,0,textToType.Length);

            textLabel.text = textToType.Substring(0, characterIndex);

            yield return null;

        }

        textLabel.text = textToType;
    }
}
