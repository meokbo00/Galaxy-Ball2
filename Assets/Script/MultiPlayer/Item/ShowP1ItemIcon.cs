using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowP1ItemIcon : MonoBehaviour
{
    public GameObject[] P1IconPlaces;
    public GameObject[] P1Icon;

    public void PrintDestroyedObjectTag(string objecttag)
    {
        GameObject nextIcon = null;

        switch (objecttag)
        {
            case "Item_Big": nextIcon = P1Icon[0]; break;
            case "Item_Small":nextIcon = P1Icon[1];break;
            case "Item_Twice":nextIcon = P1Icon[2];break;
            case "Item_Endless":nextIcon = P1Icon[3];break;
            case "Item_Invincible": nextIcon = P1Icon[4];break;
            case "Item_BlackHole": nextIcon = P1Icon[5]; break;
        }
        if (nextIcon != null)
        {
            foreach (GameObject place in P1IconPlaces)
            {
                if (place.transform.childCount == 0)
                {
                    Instantiate(nextIcon, place.transform.position, Quaternion.identity, place.transform);
                    return;
                }
            }
        }
    }
}