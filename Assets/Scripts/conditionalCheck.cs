using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conditionalCheck : MonoBehaviour
{
    public bool PlayerInTrigger = false;
    public GameObject missionItem;
    public GameObject playerChar;
    public GameObject voxCamera;
    public GameObject canvasNo;
    public GameObject canvasShow;
    public GameObject leaveChat;
    public bool playerLeftTrigger = true;
    
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(!missionItem.activeSelf){
          canvasNo.SetActive(false);
          canvasShow.SetActive(true);
        }

        if(PlayerInTrigger && playerLeftTrigger){
            playerChar.SetActive(false);
            voxCamera.SetActive(true);
            

            if(leaveChat.activeSelf && Input.GetKeyDown(KeyCode.Space)){
              playerChar.SetActive(true);
              voxCamera.SetActive(false);
              playerLeftTrigger = false;
            }
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
        playerLeftTrigger = true;
    }
  }
}
