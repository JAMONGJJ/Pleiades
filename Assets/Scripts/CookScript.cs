using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// 데이터베이스에서 레시피들을 로드해서 보여주고, 요리 기능이 구현된 스크립트
public class CookScript : MonoBehaviour
{
    [HideInInspector]
    public int code;                                            // 주문 버튼 중 눌린 버튼의 레시피 코드값을 저장하는 변수
    [HideInInspector]
    public int orderNum;                                    // OrderButtonScript로부터 받아온 주문번호(요리가 끝나면 해당 값으로 client를 찾아서 이 후 동작 수행)

    public GameObject IngredientPrefab;             // 재료버튼 프리팹
    public GameObject HitpointPrefab;                // 히트포인트 프리팹
    public GameObject IngredientsParent;            // 재료버튼 생성해서 자식으로 종속시킬 오브젝트(정리 용이 위함)
    public GameObject HitpointParent;               // 히트포인트 버튼 생성해서 자식으로 종속시킬 오브젝트(정리 용이 위함)
    public GameObject CookingSlider;                 // 요리 시작했을 때 사용하는 슬라이더
    public GameObject RecipeImage;                  // 화면 중앙에 출력되는 음식 이미지
    public GameObject RecipeName;                   // 화면 상단에 출력되는 음식 이름
    public GameObject CookButton;                   // 슬라이더값을 특정 구간에 도달했을 때 눌러야하는 버튼
    public TextMeshProUGUI timer;                    // 요리 시작하기 전에 출력되는 타이머
    public GameObject SM;                               // 시스템 메세지 오브젝트
    public TextMeshProUGUI systemMessage;       // 시스템 메세지 텍스트
    [HideInInspector]
    public bool flag = false;                               // true : 재료버튼 드래그해서 가운데 레시피 이미지로 옮기는 단계, value값 증가 멈춤 / false : 게이지맞춰서 버튼 누르는 단계, value값 증가

    private float speed;                                    // 슬라이더 value값 증가하는 속도. 0.1f로 초기화
    private Recipe recipe;                                  // 요리 레시피
    private Dictionary<int, int> ingredients;           // 요리 재료
    private float completeRatio;                        // 요리 완성도. 0.8f이상이어야 요리 성공
    private Vector3[] ingredientPosition = { new Vector3(-600.0f, 300.0f, 0.0f), new Vector3(600.0f, 300.0f, 0.0f), new Vector3(-600.0f, 100.0f, 0.0f),
                                                        new Vector3(-600.0f, 100.0f, 0.0f), new Vector3(-600.0f, -100.0f, 0.0f), new Vector3(600.0f, 100.0f, 0.0f),};
    private Vector3[] hitpointPosition = { new Vector3(300.0f, 0.0f, 0.0f), new Vector3(600.0f, 0.0f, 0.0f), new Vector3(900.0f, 0.0f, 0.0f) };

    private List<GameObject> ingredientButtonList;      // 요리 UI가 켜졌을 때 출력되는 재료 버튼들 리스트
    private List<GameObject> hitpointList;                  // 슬라이더 위에 구간 표시해주는 히트포인트들 리스트

    float time;
    int hitCount;                                                   // 히트포인트에서 버튼이 눌린 횟수
    bool cookStart;
    bool cookFinished;
    int FailorSuccess;                                              // 1 : 요리 성공, 2: 요리 실패


    private void OnEnable()
    {
        FailorSuccess = 0;
        hitpointList = new List<GameObject>();
        ingredientButtonList = new List<GameObject>();
        time = 0.0f;
        completeRatio = 0.0f;
        speed = 0.1f;
        hitCount = 0;
        cookStart = false;
        cookFinished = false;
        CookingSlider.GetComponent<Slider>().value = 0.0f;
        
        IngredientsParent.SetActive(false);
        HitpointParent.SetActive(false);
        RecipeImage.SetActive(false);
        CookingSlider.SetActive(false);
        CookButton.SetActive(false);
        RecipeName.SetActive(false);
        timer.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        ingredientButtonList.Clear();
        hitpointList.Clear();
    }

    private void FixedUpdate()
    {
        if (!cookFinished)
        {
            if (completeRatio >= 0.8f)
            {
                CookSuccess();
            }
            else
            {
                if (hitCount == 3)
                {
                    CookFail();
                }
            }
        }

        if (cookStart)
        {
            CookingSlider.GetComponent<Slider>().value += Time.deltaTime * speed;
            if (CookingSlider.GetComponent<Slider>().value >= 1.0f)
                CookFail();
        }
        else
        {
            if (time <= 3.0f)
            {
                time += Time.deltaTime;
                timer.text = ((int)(4.0f - time)).ToString();
            }
            else
            {
                cookStart = true;
                timer.gameObject.SetActive(false);
                RecipeImage.SetActive(true);
                IngredientsParent.SetActive(true);
                HitpointParent.SetActive(true);
                CookingSlider.SetActive(true);
                CookButton.SetActive(true);
                RecipeName.SetActive(true);
            }
        }
    }

    public void LoadRecipe()
    {
        DatabaseScript.SearchRecipebyCode(ref recipe, code);
        RecipeImage.GetComponent<Image>().sprite = recipe.image;
        RecipeName.GetComponent<TextMeshProUGUI>().text = recipe.name;
        ingredients = recipe.ingredients;

        SetIngredientButtons();
        SetHitpoints();
    }

    // Cook UI가 켜지는 초기에 재료버튼들을 정해진 위치에 배치하는 함수
    private void SetIngredientButtons()
    {
        int i = 0;
        foreach(var pair in ingredients)
        {
            Item tmpItem = new Item();
            DatabaseScript.SearchItembyCode(ref tmpItem, pair.Key);
            var tmp = Instantiate(IngredientPrefab, ingredientPosition[i], Quaternion.identity);
            tmp.transform.SetParent(IngredientsParent.transform, false);
            tmp.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = tmpItem.image;
            ingredientButtonList.Add(tmp);
            i++;
        }
    }

    // 재료 갯수에 따라 히트포인트 배치하는 함수
    private void SetHitpoints()
    {
        switch (ingredients.Count)
        {
            case 1:
                for (int i = 0; i < 1; i++)
                {
                    var tmp = Instantiate(HitpointPrefab, hitpointPosition[1], Quaternion.identity);
                    tmp.transform.SetParent(HitpointParent.transform, false);
                    hitpointList.Add(tmp);
                }
                break;
            case 2:
                for (int i = 0; i < 2; i++)
                {
                    var tmp = Instantiate(HitpointPrefab, hitpointPosition[i], Quaternion.identity);
                    tmp.transform.SetParent(HitpointParent.transform, false);
                    hitpointList.Add(tmp);
                }
                break;
            case 3:
            case 4:
            case 5:
            case 6:
                for (int i = 0; i < 3; i++)
                {
                    var tmp = Instantiate(HitpointPrefab, hitpointPosition[i], Quaternion.identity);
                    tmp.transform.SetParent(HitpointParent.transform, false);
                    hitpointList.Add(tmp);
                }
                break;
            default:

                break;
        }
    }

    // 슬라이더 게이지가 히트포인트 위치와 일치했을때 버튼이 눌렸는지 검사하는 함수
    public void HitpointTouched()
    {
        var tmpSlider = CookingSlider.GetComponent<Slider>();
        float width = tmpSlider.GetComponent<RectTransform>().sizeDelta.x;
        float tmpX = hitpointList[hitCount].GetComponent<RectTransform>().anchoredPosition.x;

        if (tmpSlider.value >= (tmpX - 25) / width && tmpSlider.value <= (tmpX + 25) / width)
        {
            if (hitpointList.Count == 1)
                completeRatio += 1.0f;
            else if (hitpointList.Count == 2)
                completeRatio += 0.5f;
            else if (hitpointList.Count == 3)
                completeRatio += 0.33f;
            hitCount++;
        }
        else if (tmpSlider.value >= (tmpX - 50) / width && tmpSlider.value <= (tmpX + 50) / width)
        {
            if (hitpointList.Count == 1)
                completeRatio += 0.9f;
            else if (hitpointList.Count == 2)
                completeRatio += 0.4f;
            else if (hitpointList.Count == 3)
                completeRatio += 0.23f;
            hitCount++;
        }
        else
        {
            CookFail();
        }
    }

    private void CookSuccess()
    {
        cookFinished = true;
        Debug.Log("Cook Success");
        FailorSuccess = 1;
        RemoveIngredients();
        DatabaseScript.cookedCuisineNumber++;
        DatabaseScript.Money += recipe.price;
        SystemMessageON(1);
    }

    private void CookFail()
    {
        cookFinished = true;
        Debug.Log("Cook Fail");
        FailorSuccess = 2;
        RemoveIngredients();
        SystemMessageON(0);
    }

    // 요리를 하는데에 필요한 재료들을 인벤토리에서 삭제하는 함수
    private void RemoveIngredients()
    {
        foreach(var i in ingredients)
        {
            DatabaseScript.Delete_Inventory(i.Key, i.Value);
        }
    }

    // 요리 종료하는 함수
    private void ExitCook()
    {
        ingredientButtonList.Clear();
        hitpointList.Clear();
        GameObject.FindWithTag("GameManager").GetComponent<UIChange_Restaurant>().UIChange();
        GameObject.FindWithTag("OrderListUI").GetComponent<OrderListScript>().RemoveOrder(orderNum, FailorSuccess);
    }

    // 상황에 맞게 시스템 메세지 출력
    private void SystemMessageON(int index)
    {
        string text = "";   //  시스템 메세지로 출력될 텍스트
        switch (index)
        {
            case 0:     // 미니게임 실패 메세지
                text = "Cook fail!\nExit minigame...";
                break;
            case 1:     // 미니게임1 성공 메세지
                text = "Cook success!\nYou earned money as a price.";
                break;
            default:
                break;
        }
        systemMessage.text = text;
        RecipeImage.SetActive(false);
        CookingSlider.SetActive(false);
        CookButton.SetActive(false);
        SM.SetActive(true);
        StartCoroutine("SystemMessageOFF_Cook");
    }
    IEnumerator SystemMessageOFF_Cook()
    {
        yield return new WaitForSeconds(2.0f);
        SM.SetActive(false);
        ExitCook();
    }
}
