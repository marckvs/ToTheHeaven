using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerContact : MonoBehaviour {

    public Light lightBall;
    public float lightIntensityMax=10;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Phiro")
        {
            Destroy(this.gameObject);
        }

    }
}
