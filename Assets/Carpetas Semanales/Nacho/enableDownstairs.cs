using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableDownstairs : MonoBehaviour {

    public PhiroMovement player;
    public GameObject platform;
    private BoxCollider2D colliderPlatform;

	// Use this for initialization
	void Start ()
    {
        colliderPlatform = platform.GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (player.stairsWithPlatform)
        {
            colliderPlatform.isTrigger = true;
        }
        else
        {
            colliderPlatform.isTrigger = false;
        }
	}
}
