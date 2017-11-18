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
    private float xMaxDistance;

    

    // Use this for initialization
    void Start () {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        Transform transform = GetComponent<Transform>();
	}
	
    private void moveVertPlatform(float y)
    {
        if (y < yMaxDistance&& 0 < y)
            transform.position = new Vector2(transform.position.x, transform.position.y + y);
        if (y < yMaxDistance && 0 > y)
            transform.position = new Vector2(transform.position.x, transform.position.y - y);
    }

    // Update is called once per frame
    void Update () {
        moveVertPlatform(yVel);
	}
}
