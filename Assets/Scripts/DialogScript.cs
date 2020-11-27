using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogScript : MonoBehaviour
{
    public string textName;
    public TextMeshProUGUI text;
    StreamReader sr;

    public void StartDialog()
    {
        sr = new StreamReader(textName + ".txt");
        ReadText();
    }

    public void ReadText()
    {
        if (sr.EndOfStream == false)
        {
            text.text = sr.ReadLine();
        }
        else
        {
            sr.Close();
            TerminateDialogUI();
        }
    }

    public void SkipDialog()
    {
        sr.Close();
        TerminateDialogUI();
    }

    private void TerminateDialogUI()
    {
        UIChange tmp = GameObject.FindWithTag("GameManager").GetComponent<UIChange>();
        tmp.DialogONOFF();
    }
}
