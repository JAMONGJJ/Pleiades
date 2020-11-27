using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


// 오른쪽 상단의 버튼(이하 메인버튼)을 눌렀을 때 메뉴 아이콘들이 노출되고, 다시 눌렀을 때 메뉴 아이콘들이 다시 회수되는 함수들 구현
public class ButtonScript : MonoBehaviour
{
    bool OpenClose;                     // 이 변수의 값에 따라 버튼이 펼쳐지거나 접힘
    bool flag;                              // 메인 버튼이 눌려서 다른 버튼들이 펼쳐질 때, 메인 버튼이 다시 눌리는 상황이 발생하면 펼쳐지던 버튼들이 중간부터 다시 접히는 버그가 발생하지 않도록 하는 변수
    public float delayTime;             // 버튼들이 펼쳐지는 시간 간격
    public Button[] buttons;        // 메뉴 버튼들 저장하는 배열
    //Animator[] animators;

    // Start is called before the first frame update
    void Start()
    {
        OpenClose = false;
        flag = false;
        //animators = new Animator[buttons.Length];
        //for (int i = 0; i < buttons.Length; i++)
        //    animators[i] = buttons[i].GetComponent<Animator>();
    }

    public void ButtonClicked()
    {
        if (flag)       // 버튼들이 펼쳐지거나 접히고 있는 상황에 메인 버튼이 다시 눌려도 버그가 발생하지 않도록 방지
            return;
        OpenClose = !OpenClose;
        flag = true;
        if (OpenClose)
            StartCoroutine("OpenButton");
        else
            StartCoroutine("CloseButton");
    }
    IEnumerator OpenButton()
    {
        foreach (var b in buttons)
        {
            b.gameObject.SetActive(true);
            yield return new WaitForSeconds(delayTime);
        }
        flag = false;       // 버튼들이 다 펼쳐짐
    }
    IEnumerator CloseButton()
    {
        foreach (var b in buttons.Reverse())
        {
            b.gameObject.SetActive(false);
            yield return new WaitForSeconds(delayTime);
        }
        flag = false;       // 버튼들이 다 접힘
    }
    //IEnumerator OpenButton2()
    //{
    //    foreach (var a in animators)
    //    {
    //        a.SetTrigger("Open");
    //        yield return new WaitForSeconds(delayTime);
    //    }
    //}
    //IEnumerator CloseButton2()
    //{
    //    foreach (var a in animators.Reverse())
    //    {
    //        a.SetTrigger("Close");
    //        yield return new WaitForSeconds(delayTime);
    //    }
    //}
}