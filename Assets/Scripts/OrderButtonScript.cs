using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderButtonScript : MonoBehaviour
{
    [HideInInspector]
    public int orderNum;
    public GameObject recipeImage;

    // 주문 버튼이 눌렸을 때, 버튼의 이미지로 DB에서 추가적인 정보를 가져와 Cook UI에 넘기고 UI 변경
    public void PassRecipeCode()
    {
        Sprite image = recipeImage.GetComponent<Image>().sprite;
        Recipe recipe = new Recipe();
        DatabaseScript.SearchRecipebyImage(ref recipe, image);
        
        GameObject.FindWithTag("GameManager").GetComponent<UIChange_Restaurant>().CookUIONOFF();
        GameObject.FindWithTag("GameManager").GetComponent<UIChange_Restaurant>().ButtonImageONOFF();
        var tmp = GameObject.FindWithTag("CookUI").GetComponent<CookScript>();
        tmp.code = recipe.code;
        tmp.orderNum = orderNum;
        tmp.LoadRecipe();
        GameObject.FindWithTag("GameManager").GetComponent<UIChange_Restaurant>().OrderListUIONOFF();
    }
}

