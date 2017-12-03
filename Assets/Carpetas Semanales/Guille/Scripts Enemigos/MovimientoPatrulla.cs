using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPatrulla : MonoBehaviour {

    public float velocidad;
    public Transform puntoA;
    public Transform puntoB;
    private Transform Phiro;
    public bool Mirar_a_Phiro_en_Patrulla;

    private Animator anim;
    private float posMin, posMax;
    private bool atacando;
    private int i;
    private float posActual;
    private float posAnterior;

    // Desplazamiento en eje X

    void Start()
    {
        Phiro = GameObject.FindGameObjectWithTag("Phiro").transform;
        anim = GetComponent<Animator>();
        atacando = false;
        i = 0;
        posActual = transform.position.x;

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

    void Update()
    {
        if (!atacando) // Si no está atacando
        {
            if (Mirar_a_Phiro_en_Patrulla) // Si enemigo mira a Phiro mientras patrulla
            {
                // Flip si Phiro está a su derecha y está mirando a la izquierda
                if (posActual < Phiro.position.x && transform.localScale.x > 0) flipEnemigo();

                // Flip si Phiro está a su izquierda y está mirando a la derecha
                if (posActual > Phiro.position.x && transform.localScale.x < 0) flipEnemigo();
            }
            else
            {
                // Flip si se mueve a la derecha y está mirando a la izquierda
                if (posActual > posAnterior && transform.localScale.x > 0) flipEnemigo();

                // Flip si se mueve a la izquierda y está mirando a la derecha
                if (posActual < posAnterior && transform.localScale.x < 0) flipEnemigo();
            }

            if (!dentroPatrulla()) // Si llega a un punto patrulla cambia dirección
            {
                velocidad = -velocidad;
                if (!Mirar_a_Phiro_en_Patrulla) flipEnemigo();
            }

            posAnterior = transform.position.x;
            desplazamientoEnemigo();
            posActual = transform.position.x;
            i = 0;
        }

        else
        {
            // Flip si Phiro está a su derecha y está mirando a la izquierda
            if (posActual < Phiro.position.x && transform.localScale.x > 0) flipEnemigo();

            // Flip si Phiro está a su izquierda y está mirando a la derecha
            if (posActual > Phiro.position.x && transform.localScale.x < 0) flipEnemigo();

            if (i++ >= 60) atacando = false; // Cuenta 1 segundo para terminar ataque sin desplazarse
        }

    }

    bool dentroPatrulla()    // Devuelve true si el enemigo no ha sobrepasado un punto patrulla
    {
        return (transform.position.x > posMin && transform.position.x < posMax);
    }

    void flipEnemigo() // Voltea la imagen del enemigo
    {
        // Si es positiva, mira a la izquierda
        // Si es negativa, mira a la derecha
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

    void OnTriggerEnter2D(Collider2D other)    // Si toca a Phiro, activa ataque
    {
        if (other.tag == "Phiro")
        {
            anim.SetTrigger("Entra_Phiro");
            atacando = true;
        }
    }

}
