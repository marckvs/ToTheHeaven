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
    private PhiroMovement pm;
    public bool climbing = false;
    public bool subir = false;


    // Use this for initialization
    void Start()
    {
        _PhiroRGD = GetComponent<Rigidbody2D>();
        pm = this.GetComponent<PhiroMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        climbing = false;
        float xvel = _PhiroRGD.velocity.x;
        bool stairs = PhiroMovement.onStairs;

        if (xvel == 0 && running == false && stairs == false && agachado == false)
        {
            phiroAnims.SetTrigger("idle");
        }

        if (Input.GetKey(KeyCode.D) && !pm.GetZip() && !pm.GetStairs() && Input.GetKey(KeyCode.A)== false)
        {
            if (!facingRight)
            {
                facingRight = true;
                phiroTransform.localScale = Vector3.Scale(phiroTransform.localScale, new Vector3(-1, 1, 1));
            }
            facingRight = true;
            phiroAnims.SetBool("running", true);
            running = true;

        }

        if (pm.zip_line == true)
        {
            phiroAnims.SetBool("tirolina", true);
        }

        if (pm.zip_line == false)
        {
            phiroAnims.SetBool("tirolina", false);
        }


        if (Input.GetKey(KeyCode.A) && !pm.GetZip() && !pm.GetStairs() && Input.GetKey(KeyCode.D) == false)
        {
            if (facingRight)
            {
                facingRight = false;
                phiroTransform.localScale = Vector3.Scale(phiroTransform.localScale, new Vector3(-1, 1, 1));
            }
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
        if (Input.GetKey(KeyCode.S) && stairs == false)
        {
            phiroAnims.SetBool("agacharse", true);
            phiroAnims.SetBool("levantarse", false);
            agachado = true;
        }


        if (Input.GetKeyUp(KeyCode.S))
        { 
            phiroAnims.SetBool("agacharse", false);
            phiroAnims.SetBool("levantarse", true);
            agachado = false;
        }
        //subir escaleras

        if (Input.GetKey(KeyCode.W) && stairs == true)
        {
            phiroAnims.SetBool("subir", true);
        }

        if (Input.GetKey(KeyCode.S) && stairs == true)
        {
            phiroAnims.SetBool("subir", true);
        }

        if (stairs == false && phiroAnims.GetBool("subir")==true)
        {
            phiroAnims.SetBool("subir", false);
        }



        //subir plataforma ("saltar")


        if (Input.GetKeyDown(KeyCode.Space) && stairs == false && running == false && pm.canClimb)
        {
            phiroAnims.SetBool("agacharse_mantener", false);
            phiroAnims.SetTrigger("estirabrazos");
            StartCoroutine(tiempoEstirarBazos());

        }

        
    }

    IEnumerator EsperandoAnimacionAgachado()
    {
        yield return new WaitForSeconds(1.0f);
        agachado = false;

    }

    public bool GetFacing() {
        return facingRight;
    }

    public bool GetAgachado()
    {
        return agachado;
    }
    void Flip()
    {
        facingRight = !facingRight;
        Debug.Log("Flip");
        transform.localPosition = _PhiroRGD.transform.position;
        phiroTransform.localScale = Vector3.Scale(phiroTransform.localScale, new Vector3(-1, 1, 1));
    }

    IEnumerator tiempoEstirarBazos()
    {
        float timer = 0.7f;
        while ((timer -= Time.deltaTime) > 0) yield return null;
        climbing = true;
        Debug.Log("climbing = true");
        phiroAnims.SetTrigger("subir_plataforma");
    }
}


