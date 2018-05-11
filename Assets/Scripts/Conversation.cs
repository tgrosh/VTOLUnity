using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Conversation : MonoBehaviour {
    public Text conversationTitle;
    public AutoType conversationText;
    public Message[] messages;

    int currentMessage = 0;
    RectTransform rect;
    CanvasGroup group;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        group = GetComponent<CanvasGroup>();

        //testing
        Message m = new Message();
        m.text = "Help me Rhonda";
        m.title = "Mr Scooter";

        Message m2 = new Message();
        m2.text = "Keep her out of my heart";
        m2.title = "Cash Me Outside";
        m2.isLeft = true;

        Message m3 = new Message();
        m3.text = "Join the social network of Tech Nerds, increase skill rank, get work, manage projects...";
        m3.title = "Led Zeppelin";

        StartConversation(new Message[] { m, m2, m3 });
    }
    	
	// Update is called once per frame
	void Update () {
        if (conversationText.typingComplete && Input.GetButtonDown("Fire1"))
        {
            if (currentMessage < messages.Length - 1)
            {
                currentMessage++;
                ShowMessage(currentMessage);
            } else
            {
                group.alpha = 0;
            }
        }
    }

    public void StartConversation(Message[] messages)
    {
        currentMessage = 0;
        this.messages = messages;
        ShowMessage(currentMessage);
        group.alpha = 1;
    }

    public void ShowMessage(int messageIndex)
    {
        conversationTitle.text = messages[messageIndex].title;
        conversationText.SetText(messages[messageIndex].text);        
        if (messages[messageIndex].isLeft)
        {
            rect.anchorMax = rect.anchorMin = rect.pivot = new Vector2(0, 0);
        } else
        {
            rect.anchorMax = rect.anchorMin = rect.pivot = new Vector2(1, 0);
        }
    }
}
