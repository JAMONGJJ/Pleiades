using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConversationScript : MonoBehaviour
{
    public TextMeshProUGUI ConversationText;
    public TextMeshProUGUI NameText;

    private string[] conversationList = { "Hi! How are you doing?", };

    private void Start()
    {
        NameText.text = "BYEOLDDANG";
        ConversationText.text = conversationList[0];
    }
}
