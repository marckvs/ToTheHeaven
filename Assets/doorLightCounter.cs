using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorLightCounter : MonoBehaviour {

    public GameObject[] lightList;
    public PhiroMovement player;
    private int lightCount;

	// Use this for initialization
	void Start ()
    {
        print(lightCount);
    }
	
	// Update is called once per frame
	void Update ()
    {
    
	}

    public void setActiveLight(int counter)
    {
        lightList[counter - 1].SetActive(true);
    }
}
