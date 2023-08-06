using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressSpaceToTalk : MonoBehaviour
{
    public bool PlayerInTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerInTrigger && Input.GetKeyDown(KeyCode.Space)){
            Debug.Log("Trigger Activated");
        }
    }

    void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player"))
    {
        PlayerInTrigger = true;
    }
  }

    void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Player"))
    {
        PlayerInTrigger = false;
    }
  }
}
