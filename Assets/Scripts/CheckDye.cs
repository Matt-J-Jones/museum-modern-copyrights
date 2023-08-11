using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckDye : MonoBehaviour
{
    public GameObject[] itemsToCollect;
    public int count;
    public GameObject switchInactive;
    public GameObject switchActive;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        count = 0;

        foreach (GameObject obj in itemsToCollect){
            if(obj.activeSelf){
                count += 1;
            }
        }

        if (count == itemsToCollect.Length){
            switchInactive.SetActive(false);
            switchActive.SetActive(true);
        }
    }
}
