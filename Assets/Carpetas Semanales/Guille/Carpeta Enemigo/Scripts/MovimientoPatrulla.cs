using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPatrulla : MonoBehaviour {

    public float velocidad;
    public Transform puntoA;
    public Transform puntoB;

    private Animator anim;
    private float posMin, posMax;
    private bool atacando;
    private int i;

    // Desplazamiento en eje X

    void Start ()
    {
        anim = GetComponent<Animator>();
        atacando = false;
        i = 0;

        // Determinar la posición mínima y máxima de patrulla y
        // asegurarse que empiece a caminar hacia punto A

        if (puntoA.position.x > puntoB.position.x)
        {
            posMin = puntoB.position.x;
            posMax = puntoA.position.x;
            if (velocidad < 0) velocidad *= -1; 
        }
        else
        {
            posMin = puntoA.position.x;
            posMax = puntoB.position.x;
            if (velocidad > 0) velocidad *= -1; 
        }
    }

    void Update ()
    {
        if (!atacando) // Solo se mueve si no está atacando
        {
            if (!dentroPatrulla())
            {
                velocidad = -velocidad;
                flipEnemigo();
            }

            desplazamientoEnemigo();
            i = 0;
        }

        else if (i++ >= 60) atacando = false; // Cuenta 1 segundo para terminar ataque
    }

    bool dentroPatrulla()    // Devuelve true si el enemigo no ha sobrepasado un punto patrulla
    {
        return (transform.position.x > posMin && transform.position.x < posMax);
    }

    void flipEnemigo() // Voltea la imagen del enemigo
    {
        transform.localScale = new Vector3(-transform.localScale.x,
                                            transform.localScale.y,
                                            transform.localScale.z);
    }

    void desplazamientoEnemigo() // Se mueve en el eje X
    {
        transform.position = new Vector3(transform.position.x + velocidad,
                                         transform.position.y,
                                         transform.position.z);
    }

    void OnTriggerEnter2D (Collider2D other)    // Si toca a Phiro, activa ataque
    {
        if (other.tag == "Phiro")
        {      
            anim.SetTrigger("Entra_Phiro");
            atacando = true;
        } 
    }
}
