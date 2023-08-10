using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{
    public Material newMaterial;
    public GameObject[] objectsToChange;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject obj in objectsToChange){
            obj.GetComponent<Renderer>().material = newMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
