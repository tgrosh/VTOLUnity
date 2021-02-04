using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public List<DialogMessage> dialogs = new List<DialogMessage>();
    
    bool moreDialog;
    int currentIndex;
    Text uiText;
    Image uiIcon;
    Image uiMore;
    Image uiPanel;

    // Start is called before the first frame update
    void Start()
    {
        uiPanel = GetComponent<Image>();
        uiText = transform.Find("DialogText").GetComponent<Text>();
        uiIcon = transform.Find("Icon").GetComponent<Image>();
        uiMore = transform.Find("MoreIcon").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        moreDialog = dialogs.Count > currentIndex + 1;
        uiText.text = dialogs[currentIndex].text;
        uiIcon.sprite = dialogs[currentIndex].icon;

        uiPanel.enabled = dialogs.Count > 0;
        uiIcon.gameObject.SetActive(dialogs.Count > 0);
        uiMore.gameObject.SetActive(moreDialog);

        if (Gamepad.current.aButton.wasPressedThisFrame || Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            MoreDialogClick();
        }
    }

    public void MoreDialogClick()
    {
        if (currentIndex < dialogs.Count - 1)
        {
            currentIndex++;
        }
    }
}
