using System.Collections;
using System.Collections.Generic;
using Picturesque.Darkbringer;
using UnityEngine;

public class PressSpaceToTalk : MonoBehaviour
{
    public bool PlayerInTrigger = false;
    public bool seenCutscene = false;
    public GameObject player;
    public GameObject voxCam;
    public GameObject activeExit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerInTrigger && Input.GetKeyDown(KeyCode.Space) && !seenCutscene){
          player.SetActive(false);
          voxCam.SetActive(true);  
          seenCutscene = true;
        }

        if(Input.GetKeyDown(KeyCode.Space) && seenCutscene && activeExit.activeSelf){
          player.SetActive(true);
          voxCam.SetActive(false); 
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
