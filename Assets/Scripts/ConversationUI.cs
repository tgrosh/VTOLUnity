using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationUI : MonoBehaviour {
    public Text conversationTitle;
    public AutoType conversationText;

    Conversation conversation;
    int currentMessage = 0;
    RectTransform rect;
    CanvasGroup group;

    void Start()
    {
        rect = GetComponent<RectTransform>();
        group = GetComponent<CanvasGroup>();

        //testing
        //Conversation testConversation = Resources.Load<Conversation>("Conversations/TestConversation");
        //StartConversation(testConversation);
    }
    	
	// Update is called once per frame
	void Update () {
        if (conversationText.typingComplete && Input.GetButtonDown("Fire1"))
        {
            if (currentMessage < conversation.messages.Count - 1)
            {
                currentMessage++;
                ShowMessage(currentMessage);
            } else
            {
                group.alpha = 0;
            }
        }
    }

    public void StartConversation(Conversation conversation)
    {
        currentMessage = 0;
        this.conversation = conversation;
        ShowMessage(currentMessage);
        group.alpha = 1;
    }

    public void ShowMessage(int messageIndex)
    {
        conversationTitle.text = conversation.messages[messageIndex].title;
        conversationText.SetText(conversation.messages[messageIndex].text);        
        if (conversation.messages[messageIndex].isLeft)
        {
            rect.anchorMax = rect.anchorMin = rect.pivot = new Vector2(0, 0);
        } else
        {
            rect.anchorMax = rect.anchorMin = rect.pivot = new Vector2(1, 0);
        }
    }
}
