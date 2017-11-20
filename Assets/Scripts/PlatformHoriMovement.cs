using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHoriMovement: MonoBehaviour {

    [SerializeField]
    private float xVel;

    [SerializeField]
    private float xMinDistance;

    [SerializeField]
    private float xMaxDistance;

    private bool cambio = true;

    // Use this for initialization
    void Start () {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        xMaxDistance += transform.position.x;
        xMinDistance += transform.position.x;
    }

    private void moveVertPlatform() {

        if (transform.position.x > xMaxDistance && cambio==true) {
            xVel = xVel * -1;
            cambio = false;
        }
        else if (transform.position.x < xMinDistance && cambio == false) {
            xVel = xVel * -1;
            cambio = true;
        }
           
        transform.position = new Vector3(transform.position.x + xVel, transform.position.y, transform.position.z);

    }

    // Update is called once per frame
    void FixedUpdate () {
        moveVertPlatform();
	}
}
