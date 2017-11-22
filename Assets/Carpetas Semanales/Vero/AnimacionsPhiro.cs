using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionsPhiro : MonoBehaviour
{

    public Animator phiroAnims;
    public bool facingRight;
    public Transform phiroTransform;
    private Rigidbody2D _PhiroRGD;

    // Use this for initialization
    void Start()
    {
        _PhiroRGD = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //float h = Input.GetAxis("Horizontal");
        /*
        float xvel = _PhiroRGD.velocity.x;
        if (xvel == 0)
        {
            phiroAnims.SetTrigger("idle");
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.A) && PhiroMovement.grounded && xvel!=0)
        {            
            if (xvel > 0)
            {
                //Flip();
                //facingRight = true;
                
                if (!facingRight) {
                    facingRight = true;
                    transform.localPosition = _PhiroRGD.transform.position;
                    phiroTransform.localScale = Vector3.Scale(phiroTransform.localScale, new Vector3(-1, 1, 1));

                }
                else { facingRight = true; }
            }
             else if(xvel < 0)
            {
                //Flip();
                //facingRight = false;
                
                if (!facingRight) {
                    facingRight = true;
                    transform.localPosition = _PhiroRGD.transform.position;
                    phiroTransform.localScale = Vector3.Scale(phiroTransform.localScale, new Vector3(-1, 1, 1));
                }
                else { facingRight = false; }
            }


            phiroAnims.SetTrigger("correr");
            Debug.Log(facingRight);

        }
       
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyDown(KeyCode.A))
        {
            phiroAnims.SetBool("CorrerD", false);
        }
        */
        float xvel = _PhiroRGD.velocity.x;
        if (xvel == 0)
        {
            phiroAnims.SetBool("running", false);
            phiroAnims.SetTrigger("idle");


        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (!facingRight)
            {
                facingRight = true;
                phiroTransform.localScale = Vector3.Scale(phiroTransform.localScale, new Vector3(-1, 1, 1));
            }
            facingRight = true;
            phiroAnims.SetBool("running", true);

        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (facingRight)
            {
                facingRight = false;
                phiroTransform.localScale = Vector3.Scale(phiroTransform.localScale, new Vector3(-1, 1, 1));
            }
            facingRight = false;
            phiroAnims.SetBool("running", true);


        }


        if (Input.GetKeyDown(KeyCode.S))
        {
            phiroAnims.SetTrigger("agacharse");

        }
        
    }

    void Flip()
    {
        facingRight = !facingRight;
        Debug.Log("Flip");
        transform.localPosition = _PhiroRGD.transform.position;
        phiroTransform.localScale = Vector3.Scale(phiroTransform.localScale, new Vector3(-1, 1, 1));


    }
}


