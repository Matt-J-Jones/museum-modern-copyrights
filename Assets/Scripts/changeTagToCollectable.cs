using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeTagToCollectable : MonoBehaviour
{
    public GameObject itemToCollect;
    void Start()
    {
        itemToCollect.transform.tag = "CollectableItem";
    }
}
