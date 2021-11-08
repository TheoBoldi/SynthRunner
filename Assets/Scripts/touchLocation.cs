using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchLocation
{
    public int touchId;
    public GameObject item;

    public touchLocation(int newTouchId, GameObject newItem)
    {
        touchId = newTouchId;
        item = newItem;
    }
}
