using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryViewer : MonoBehaviour
{
    public GameObject portraitPrefab;
    public ItemList ItemList;

    private void Update()
    {
        if (Inventory.gotItem)
        {
            UpdateInventory();
            Inventory.gotItem = false;
        }
    }

    public void UpdateInventory()
    {
        GameObject temp = Instantiate(portraitPrefab, this.transform);       
        temp.transform.Find("ItemSprite").GetComponent<Image>().sprite = ItemList.items[ItemList.FindItemByID(Inventory.GetLastItem())].image;
    }
}
