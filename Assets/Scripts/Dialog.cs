using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public DialogMessage[] dialogs;
    
    bool moreDialog;
    int currentIndex;
    Text uiText;
    Image uiIcon;
    Image uiMore;

    // Start is called before the first frame update
    void Start()
    {
        uiText = transform.Find("DialogText").GetComponent<Text>();
        uiIcon = transform.Find("Icon").GetComponent<Image>();
        uiMore = transform.Find("MoreIcon").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        moreDialog = dialogs.Length > currentIndex + 1;
        uiText.text = dialogs[currentIndex].text;
        uiIcon.sprite = dialogs[currentIndex].icon;
    }

    public void MoreDialogClick()
    {
        currentIndex++;
    }
}
