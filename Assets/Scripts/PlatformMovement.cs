using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement: MonoBehaviour {

    [SerializeField]
    private float yVel;

    [SerializeField]
    private float xVel;

    [SerializeField]
    private float yMaxDistance;

    [SerializeField]
    private float yMinDistance;

    [SerializeField]
    private float xMaxDistance;

    

    // Use this for initialization
    void Start () {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        Transform transform = GetComponent<Transform>();
        yMaxDistance += transform.position.y;
        yMinDistance += transform.position.y;
	}
	
    private void moveVertPlatform(float y)
    {

        if (transform.position.y < yMaxDistance && transform.position.y > yMinDistance)
            transform.position += new Vector3(0, y, 0);
        else y = -y;
    }

    // Update is called once per frame
    void FixedUpdate () {
        moveVertPlatform(yVel);
	}
}
