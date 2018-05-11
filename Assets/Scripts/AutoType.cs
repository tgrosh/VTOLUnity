using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class AutoType : MonoBehaviour {
    public AudioSource audioSource;
    public float letterDelay = 0.2f;
    public AudioClip sound;

    string message;
    StringBuilder currentText = new StringBuilder();
    Text uiText;

    // Use this for initialization
    void Start()
    {
        uiText = GetComponent<Text>();
        SetText(uiText.text);
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            StopCoroutine("TypeText");
            uiText.text = message;
        }
    }

    public void SetText(string text)
    {
        StopCoroutine("TypeText");
        currentText = new StringBuilder();
        message = text;
        uiText.text = "";
        StartCoroutine("TypeText");
    }

    IEnumerator TypeText()
    {
        foreach (char letter in message.ToCharArray())
        {
            currentText.Append(letter);
            uiText.text = currentText.ToString();
            if (audioSource && sound)
            {
                audioSource.PlayOneShot(sound);
                yield return 0;
            }
            yield return new WaitForSeconds(letterDelay);
        }
    }
}
