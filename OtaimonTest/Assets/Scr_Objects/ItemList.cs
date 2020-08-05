using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class ItemList : ScriptableObject
{
    public List<Item> items;

    public short FindItemByID(short id)
    {
        short index = -1;
        for (short i = 0; i < items.Count; i++)
        {
            if (id == items[i].id)
            {
                index = i;
                break;
            }
        }
        return index;
    }
}
