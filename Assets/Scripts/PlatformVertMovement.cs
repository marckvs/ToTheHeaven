using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformVertMovement: MonoBehaviour {

    [SerializeField]
    private float yVel;

    [SerializeField]
    private float yMaxDistance;

    [SerializeField]
    private float yMinDistance;


    private bool cambio = true;

    // Use this for initialization
    void Start () {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        yMaxDistance += transform.position.y;
        yMinDistance += transform.position.y;
    }


    private void moveVertPlatform() {

        if (transform.position.y > yMaxDistance && cambio==true) {
            yVel = yVel * -1;
            cambio = false;
        }
        else if (transform.position.y < yMinDistance && cambio == false) {
            yVel = yVel * -1;
            cambio = true;
        }
           
        transform.position = new Vector3(transform.position.x, transform.position.y + yVel, transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate () {
        moveVertPlatform();
	}
}
