using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionsPhiro : MonoBehaviour
{

    public Animator phiroAnims;
    public bool facingRight = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            phiroAnims.SetBool("CorrerI", false);
            float h = Input.GetAxis("Horizontal");
            if (h > 0 && !facingRight)
                Flip();
            phiroAnims.SetBool("CorrerD", true);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            phiroAnims.SetBool("CorrerD", false);
            float h = Input.GetAxis("Horizontal");
            if (h < 0 && facingRight)
                Flip();
            phiroAnims.SetBool("CorrerI", true);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            phiroAnims.SetBool("CorrerD", false);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            phiroAnims.SetBool("CorrerI", false);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            phiroAnims.SetTrigger("agacharse");
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}


