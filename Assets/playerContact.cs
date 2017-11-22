using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerContact : MonoBehaviour {

    public Light lightBall;
    public bool reducir;
    public float lightIntensityMax=10;

	// Use this for initialization
	void Start ()
    {
        reducir = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (lightBall.intensity > lightIntensityMax)
        {
            reducir = true;
        }

        if (lightBall.intensity == 0)
        {
            reducir = false;
        }

        if (reducir)
        {
            lightBall.intensity -= 0.1f;
        }

        if (!reducir)
        {
            lightBall.intensity += 0.1f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Phiro")
        {
            Destroy(this.gameObject);
        }

    }
}
