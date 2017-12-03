using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogues : MonoBehaviour {

    public GameObject GO;

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Phiro")
        {
            
        }
    }
	void OnTriggerStay2D(Collider2D c)
    {
        if (c.gameObject.tag == "Phiro" && Input.GetKeyDown(KeyCode.F)) 
        {
            GO.SetActive(true);
        }
    }
}
