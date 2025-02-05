using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowP2ItemIcon : MonoBehaviour
{
    public GameObject[] P2IconPlaces;
    public GameObject[] P2Icon;

    public void PrintDestroyedObjectTag(string objecttag)
    {
        GameObject nextIcon = null;

        switch (objecttag)
        {
            case "Item_Big": nextIcon = P2Icon[0]; break;
            case "Item_Small": nextIcon = P2Icon[1]; break;
            case "Item_Twice": nextIcon = P2Icon[2]; break;
            case "Item_Endless": nextIcon = P2Icon[3]; break;
            case "Item_Invincible": nextIcon = P2Icon[4]; break;
            case "Item_BlackHole": nextIcon = P2Icon[5]; break;
        }

        if (nextIcon != null)
        {
            foreach (GameObject place in P2IconPlaces)
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