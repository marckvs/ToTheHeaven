using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerContact : MonoBehaviour {

    public Light lightBall;
    public bool reducir;
    public float lightIntensityMax = 10;
    public float lightIntensityMin = 7;

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

        if (lightBall.intensity < lightIntensityMin)
        {
            reducir = false;
        }

        if (reducir)
        {
            lightBall.intensity -= 0.05f;
        }

        if (!reducir)
        {
            lightBall.intensity += 0.05f;
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
