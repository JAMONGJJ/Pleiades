using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DictionaryScript : MonoBehaviour
{
    public GameObject itemPrefab;
    List<GameObject> items;
    public GameObject recipePrefab;
    List<GameObject> recipes;
    public GameObject itemsContent;
    public GameObject recipesContent;
    RectTransform RT;

    private void OnEnable()
    {
        items = new List<GameObject>();
        recipes = new List<GameObject>();
        ViewItems();
        ViewRecipe();
    }

    private void OnDisable()
    {
        foreach (var o in items)
            Destroy(o.gameObject);
        foreach (var o in recipes)
            Destroy(o.gameObject);
        Resources.UnloadUnusedAssets();
    }

    // Dictionary 화면에 보여질 Items 목록 세팅
    void ViewItems()
    {
        RT = itemsContent.GetComponent<RectTransform>();
        int itemsCount = DatabaseScript.ItemsLength();

        int width = itemsCount * 500;
        int posX = (width - 1200) / 2;
        RT.anchoredPosition = new Vector2(posX, 0.0f);
        RT.sizeDelta = new Vector2(width, 850);

        Vector3 startPos = new Vector3((itemsCount - 1) * (-250), 0.0f, 0.0f);
        for(int i = 0; i < itemsCount; i++)
        {
            var tmp = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            tmp.transform.SetParent(itemsContent.transform, false);
            tmp.GetComponent<RectTransform>().anchoredPosition = new Vector2(startPos.x + i * 500.0f, 0.0f);
            tmp.transform.GetChild(0).GetComponent<Image>().sprite = DatabaseScript.ITEMS[i].image;
            tmp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = DatabaseScript.ITEMS[i].name;
            tmp.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = DatabaseScript.ITEMS[i].info;
            items.Add(tmp);
        }
    }

    // Dictionary 화면에 보여질 Recipes 목록 세팅
    void ViewRecipe()
    {
        RT = recipesContent.GetComponent<RectTransform>();
        int recipesCount = DatabaseScript.RecipesLength_ALL();

        int width = recipesCount * 500;
        int posX = (width - 1200) / 2;
        RT.anchoredPosition = new Vector2(posX, 0.0f);
        RT.sizeDelta = new Vector2(width, 850);

        Vector3 startPos = new Vector3((recipesCount - 1) * (-250), 0.0f, 0.0f);
        for (int i = 0; i < recipesCount; i++)
        {
            var tmp = Instantiate(recipePrefab, Vector3.zero, Quaternion.identity);
            tmp.transform.SetParent(recipesContent.transform, false);
            tmp.GetComponent<RectTransform>().anchoredPosition = new Vector2(startPos.x + i * 500.0f, 0.0f);
            tmp.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = DatabaseScript.RECIPES[i].image;
            tmp.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = DatabaseScript.RECIPES[i].name;
            tmp.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = DatabaseScript.RECIPES[i].info;
            recipes.Add(tmp);
        }
    }
}
