using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchInteract : MonoBehaviour
{
    public Button portalButton;
    public Button dialogButton;
    public Button minigameButton;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RestaurantPortal")
        {
            portalButton.gameObject.SetActive(true);
        }
        else if (other.tag == "NPC")
        {
            dialogButton.gameObject.SetActive(true);
        }
        else if (other.tag == "MinigameNPC")
        {
            minigameButton.gameObject.SetActive(true);
        }
        else if (other.tag == "ShopNPC")
        {

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "RestaurantPortal")
        {
            portalButton.gameObject.SetActive(false);
        }
        else if (other.tag == "NPC")
        {
            dialogButton.gameObject.SetActive(false);
        }
        else if (other.tag == "MinigameNPC")
        {
            minigameButton.gameObject.SetActive(false);
        }
        else if (other.tag == "ShopNPC")
        {

        } 
    }
}
