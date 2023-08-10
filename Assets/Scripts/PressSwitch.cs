using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressSwitch : MonoBehaviour
{
    public bool PlayerInTrigger = false;
    public GameObject switchOff;
    public GameObject switchOn;
    public GameObject tubeGoo;
    // Start is called before the first frame update
    void Start(){}

    // Update is called once per frame
    void Update()
    {
        if(PlayerInTrigger && Input.GetKeyDown(KeyCode.Space) && switchOff.activeSelf){
            switchOff.SetActive(false);
            switchOn.SetActive(true);
            tubeGoo.SetActive(true);
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
