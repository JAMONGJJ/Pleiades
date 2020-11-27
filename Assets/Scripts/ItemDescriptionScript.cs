using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDescriptionScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector]
    public Item item;

    InventoryScript ivScript;

    // Start is called before the first frame update
    private void OnEnable()
    {
        ivScript = GameObject.FindWithTag("Inventory").GetComponent<InventoryScript>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.pointerCurrentRaycast.gameObject.name == "itemImage")
        {
            if(item.code != -1)
            {
                ivScript.ItemDescription(item);
                ivScript.descriptionBG.SetActive(true);
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.name == "itemImage")
        {
            if (item.code != -1)
            {
                ivScript.descriptionBG.SetActive(false);
            }
        }
    }
}
