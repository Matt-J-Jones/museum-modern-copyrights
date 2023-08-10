using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPuzzle : MonoBehaviour
{
    public bool PlayerInTrigger = false;
    public GameObject switchOff;
    public GameObject switchOn;
    public GameObject[] lights;

    // Update is called once per frame
    void Update()
    {
        if(PlayerInTrigger && Input.GetKeyDown(KeyCode.Space)){
            switchOff.SetActive(false);
            switchOn.SetActive(true);
            foreach(GameObject obj in lights){
                if (obj.activeSelf){ obj.SetActive(false); }
                else { obj.SetActive(true); }
            }
        }

        if(Input.GetKeyUp(KeyCode.Space)){
            switchOff.SetActive(true);
            switchOn.SetActive(false);
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
