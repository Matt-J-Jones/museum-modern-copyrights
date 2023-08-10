using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class giveItemToPerson : MonoBehaviour
{
    public bool PlayerInTrigger = false;
    public bool seenCutscene = false;
    public GameObject missionItem;
    public GameObject player;
    public GameObject voxCam;
    public GameObject activeExit;
    public GameObject blockingWall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if(PlayerInTrigger && Input.GetKeyDown(KeyCode.Space) && !missionItem.activeSelf){
          if (!voxCam.activeSelf) {
            player.SetActive(false);
            voxCam.SetActive(true);
            seenCutscene = true; 
          } else {
            player.SetActive(true);
            voxCam.SetActive(false); 
          }
       }

       if(seenCutscene){
          blockingWall.SetActive(false);
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
