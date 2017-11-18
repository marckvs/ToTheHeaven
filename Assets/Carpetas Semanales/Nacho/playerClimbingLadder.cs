using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerClimbingLadder : MonoBehaviour {

    public PhiroMovement player;
    public Collider2D colliderPlatform;

    void Start()
    {
        player = FindObjectOfType<PhiroMovement>();
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
