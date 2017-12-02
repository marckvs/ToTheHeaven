using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableDownstairs : MonoBehaviour {

    private GameObject player;
    private PhiroMovement phiroMov;
    public GameObject platform;
    private BoxCollider2D colliderPlatform;

	// Use this for initialization
    
	void Start ()
    {
        colliderPlatform = platform.GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Phiro");
        phiroMov = player.GetComponent<PhiroMovement>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (phiroMov.stairsWithPlatform)
        {
            colliderPlatform.isTrigger = true;
        }
        else
        {
            colliderPlatform.isTrigger = false;
        }
	}
}
