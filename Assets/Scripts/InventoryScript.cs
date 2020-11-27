using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public Sprite defaultImage;
    public GameObject itemInfo;
    public GameObject standard;                         // items 리스트에 속한 이미지들의 부모 오브젝트(정리의 용이함을 위해)
    public Dictionary<int, int> Inventory;                  // DB에 있는 Inventory 리스트에서 레퍼삼아 데이터 받아오는 리스트
    public TextMeshProUGUI moneyText;
    public GameObject descriptionBG;                    // 인벤토리의 아이템을 터치하면 왼쪽에 나타나는, 큰 이미지와 아이템 설명을 포함하고 있는 오브젝트
    public static int maxCapacity = 30;

    private List<GameObject> items;                     // 캔버스에 뿌려지는 아이템들의 이미지와 갯수 정보

    private void OnEnable()
    {
        Inventory = new Dictionary<int, int>();
        DatabaseScript.RenewalInventory(ref Inventory);
        items = new List<GameObject>();
        Vector3 initialPos = new Vector3(-325.0f, 300.0f, 0.0f);
        for(int i = 0; i < maxCapacity; i++)
        {
            int row = i % 6;
            int column = i / 6;
            var tmp = Instantiate(itemInfo, initialPos + new Vector3(row * 150.0f, -column * 150.0f, 0.0f), Quaternion.identity);
            tmp.transform.SetParent(standard.transform, false);
            items.Add(tmp);
        }
    }

    private void OnDisable()
    {
        foreach (var o in items)
            Destroy(o.gameObject);
        Resources.UnloadUnusedAssets();
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < maxCapacity; i++)
        {
            if (i < Inventory.Count)
            {
                Item item = new Item();
                if(DatabaseScript.SearchItembyCode(ref item, Inventory.Keys.ElementAt(i)))
                {
                    items[i].GetComponent<ItemDescriptionScript>().item = item;
                    items[i].transform.GetChild(0).GetComponent<Image>().sprite = item.image;
                    items[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Inventory[i].ToString();
                }
            }
            else
            {
                items[i].GetComponent<ItemDescriptionScript>().item.code = -1;
                items[i].transform.GetChild(0).GetComponent<Image>().sprite = defaultImage;
                items[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = null;
            }
        }

        moneyText.text = DatabaseScript.Money.ToString();
    }

    public void ItemDescription(Item item)
    {
        descriptionBG.transform.GetChild(0).GetComponent<Image>().sprite = item.image;
        descriptionBG.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.info;
    }
}
