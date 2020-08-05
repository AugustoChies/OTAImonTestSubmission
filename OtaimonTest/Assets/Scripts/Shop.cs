using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop
{
    public static List<short> itemIds;

    public static bool open, ready;

    public static void Setup(ItemList itemList)
    {
        open = false;
        itemIds = new List<short>();

        int amount = Random.Range(3, 6);

        for (int i = 0; i < amount; i++)
        {
            int index = Random.Range(0, itemList.items.Count);
            itemIds.Add(itemList.items[index].id);
            // Randomize each item's cost and type of currency 
            itemList.items[index].costAmount = (short)Random.Range(1, 6);
            itemList.items[index].costType = (CurrencyColor)Random.Range(0, (CurrencyColor.GetValues(typeof(CurrencyColor)).Length));
        }
        ready = true;
    }

    public static bool CheckEmpty()
    {
        if(itemIds == null)
        {
            return false;
        }
        for (int i = 0; i < itemIds.Count; i++)
        {
            if (itemIds[i] != -1)
            {
                return false;
            }
        }
        return true;
    }
}
