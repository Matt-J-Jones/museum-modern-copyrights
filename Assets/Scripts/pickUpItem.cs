using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpItem : MonoBehaviour
{
    public bool PlayerInTrigger = false;
    public GameObject pickUpText;
    public GameObject itemToCollect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
          if (PlayerInTrigger && gameObject.CompareTag("CollectableItem"))
      {
          pickUpText.SetActive(true);
      }
      else
      {
          pickUpText.SetActive(false);
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
