using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Inventory
{
    public static int[] money;
    // Stores the IDs of owned items
    public static List<short> items;
    // Signals an currency update
    public static bool updated;
    public static bool gotItem;

    public static short GetLastItem()
    {
        return items[items.Count - 1];
    }
}
