using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelInventory : MonoBehaviour
{
   
    public Transform inventoryButton;
    public Transform inventoryContent;
    AllItems allItems;

    private void Awake()
    {
        allItems = FindAnyObjectByType<AllItems>();
    }
    private void Start()
    {
        //addAllItemsToInventory();
    }
    public void addAllItemsToInventory()
    {

        for (int i = 0; i < allItems.allItemInventorySprites.Length; i++)
        {
            GameObject newbutton = Instantiate(inventoryButton, inventoryContent).gameObject;
            newbutton.GetComponent<Image>().sprite = allItems.allItemInventorySprites[i];
        }
    }
    public void removeAllItemsFromInventory()
    {

        foreach (Transform inventoryItem in inventoryContent.GetComponentsInChildren<Transform>())
        {
            Destroy(inventoryItem);
        }
    }


    public void addToInventory(string _itemName)
    {

        switch (_itemName)
        {
            case "Redstone":
                GameObject newbutton = Instantiate(inventoryButton, inventoryContent).gameObject;
                newbutton.GetComponent<Image>().sprite = allItems.allItemInventorySprites[0];
                break;

            default:
                break;
        }


    }

}
