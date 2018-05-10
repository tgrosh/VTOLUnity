using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoType : MonoBehaviour {
    public AudioSource audioSource;
    public float letterDelay = 0.2f;
    public AudioClip sound;

    string message;
    Text text;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        message = text.text;
        text.text = "";
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        foreach (char letter in message.ToCharArray())
        {
            text.text += letter;
            if (audioSource && sound)
                audioSource.PlayOneShot(sound);
            yield return 0;
            yield return new WaitForSeconds(letterDelay);
        }
    }
}
