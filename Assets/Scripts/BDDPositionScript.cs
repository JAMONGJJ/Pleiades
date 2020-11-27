using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 씬이 전환될 때 별땅이의 위치를 변경해주는 스크립트
public class BDDPositionScript : MonoBehaviour
{
    public static int index;                                            // 위치 인덱스 (1 ; Restaurant->Village, 2 : Minigame->Village)

    static Vector3 startPos;
    GameObject player;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if (player = GameObject.FindWithTag("Player"))  // Village에만 Player태그를 사용하기 때문에 Village 씬으로 넘어간거로 확인
        {
            PlayerPosition(index);
            player.transform.position = startPos;
            GameObject.FindWithTag("GameManager").GetComponent<UIChange>().ResetCanvas();
            this.enabled = false;
        }
    }

    public static void PlayerPosition(int index)
    {
        switch (index)
        {
            case 1:                     // 레스토랑에서 Village로 넘어갈 때 시작 위치
                startPos = GameObject.FindWithTag("RestaurantPortal").transform.position;
                break;
            case 2:                     // 미니게임에서 Village로 넘어갈 때 시작 위치
                startPos = GameObject.FindWithTag("MinigamePortal").transform.position;
                break;
            default:

                break;
        }
    }
}
