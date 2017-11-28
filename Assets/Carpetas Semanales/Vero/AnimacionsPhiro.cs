using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacionsPhiro : MonoBehaviour
{

    public Animator phiroAnims;
    public bool facingRight;
    public Transform phiroTransform;
    private Rigidbody2D _PhiroRGD;
    private bool running = false;
    private bool agachado = false;


    // Use this for initialization
    void Start()
    {
        _PhiroRGD = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void LateUpdate()
    {

        float xvel = _PhiroRGD.velocity.x;
        bool stairs = PhiroMovement.onStairs;

        if (xvel == 0 && running == false && stairs == false)
        {
            agachado = false;
            phiroAnims.SetTrigger("idle");
        }
        if (Input.GetKeyDown(KeyCode.D) )
        {
            if (!facingRight)
            {
                facingRight = true;
                phiroTransform.localScale = Vector3.Scale(phiroTransform.localScale, new Vector3(-1, 1, 1));
            }
            agachado = false;
            facingRight = true;
            phiroAnims.SetBool("running", true);
            running = true;

        }
        if (Input.GetKeyDown(KeyCode.A) )
        {
            if (facingRight)
            {
                facingRight = false;
                phiroTransform.localScale = Vector3.Scale(phiroTransform.localScale, new Vector3(-1, 1, 1));
            }
            agachado = false;
            facingRight = false;
            phiroAnims.SetBool("running", true);
            running = true;


        }

        if (Input.GetKey(KeyCode.D)== false && Input.GetKey(KeyCode.A)==false)
        {
            phiroAnims.SetBool("running", false);
            running = false;
        }

        //agacharse
        if (Input.GetKeyDown(KeyCode.S) && stairs == false)
        {
            phiroAnims.SetTrigger("agacharse");
            agachado = true;

        }

        //subir escaleras

        if (Input.GetKeyDown(KeyCode.W) && stairs == true)
        {
            phiroAnims.SetBool("subir", true);
        }

        if (Input.GetKeyDown(KeyCode.S) && stairs == true)
        {
            phiroAnims.SetBool("subir", true);
        }

        if (stairs == false && phiroAnims.GetBool("subir")==true)
        {
            phiroAnims.SetBool("subir", false);
            agachado = false;
        }

        //subir plataforma ("saltar")

        if (Input.GetKeyDown(KeyCode.Space) && stairs == false && agachado == false && running == false)
        {
            phiroAnims.SetTrigger("estirabrazos");

           /* if (PhiroMovement.subeplataforma)
            {
                phiroAnims.SetBool("hasubido", true);
            }*/

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


