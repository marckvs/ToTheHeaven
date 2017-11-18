using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
    private Transform phiro;
    private SpriteRenderer background;
    private Vector2 limits;
    private Vector2 bg_bounds;
    private bool following_x_axis;
    private bool following_y_axis;

	void Start () {
        phiro = GameObject.Find("Phiro").GetComponent<Transform>();
        background = GameObject.Find("background").GetComponent<SpriteRenderer>();
        limits = new Vector2(Camera.main.aspect*Camera.main.orthographicSize, Camera.main.orthographicSize);
        bg_bounds = new Vector2(background.sprite.bounds.size.x/2, background.sprite.bounds.size.y/2);
    }

    void Update () {
        transform.position = new Vector3(phiro.position.x, phiro.position.y, -10);
        if(transform.position.x >= bg_bounds.x - limits.x)
        {
            transform.position = new Vector3(bg_bounds.x - limits.x, transform.position.y, -10);
        }
        else if(transform.position.x <= -bg_bounds.x + limits.x)
        {
            transform.position = new Vector3(-bg_bounds.x + limits.x, transform.position.y, -10);
        }
        else
        {
            transform.position = new Vector3(phiro.position.x, transform.position.y, -10);
        }
        if (transform.position.y >= bg_bounds.y - limits.y)
        {
            transform.position = new Vector3(transform.position.x, bg_bounds.y - limits.y, -10);
        }
        else if (transform.position.y <= -bg_bounds.y + limits.y)
        {
            transform.position = new Vector3(transform.position.x, -bg_bounds.y + limits.y, -10);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, phiro.position.y, -10);
        }
    }
}
