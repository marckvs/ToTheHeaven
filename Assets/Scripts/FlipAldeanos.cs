using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipAldeanos : MonoBehaviour {

    public bool Está_Mirando_a_la_Derecha;

    private Transform Phiro;
    private float pos;
    private bool der;

	void Start () {
        pos = transform.position.x;
        Phiro = GameObject.FindGameObjectWithTag("Phiro").transform;
        if (Está_Mirando_a_la_Derecha) der = true;
        else der = false;
    }
	
	void Update () {

        // Flip si npc está mirando a la derecha y Phiro está a la izquierda
        if (der && pos > Phiro.position.x)
        {
            flipNPC();
            der = false;
        }
        else if (!der && pos < Phiro.position.x)
        {
            flipNPC();
            der = true;
        }
		
	}

    void flipNPC() // Voltea la imagen del npc
    {
        transform.localScale = new Vector3(-transform.localScale.x,
                                            transform.localScale.y,
                                            transform.localScale.z);
    }

}
