using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoVolador : MonoBehaviour {

    public float velocidad;
    public Transform puntoA;
    public Transform puntoB;
    public Transform puntoC;
    public Transform Phiro;

    private Animator anim;
    private float posMin, posMax;
    private bool atacando;
    private int i;
    private float patrullaX, patrullaY;
    private bool persiguiendo;
    private float absVel;

    // Desplazamiento en eje X

    void Start()
    {
        anim = GetComponent<Animator>();
        atacando = false;
        persiguiendo = false;
        i = 0;
        patrullaX = puntoC.position.x;
        patrullaY = puntoC.position.y;
        absVel = Mathf.Abs(velocidad);

        // Determinar la posición mínima y máxima de patrulla y
        // asegurarse que empiece a caminar hacia punto A

        if (puntoA.position.y > puntoB.position.y)
        {
            posMin = puntoB.position.y;
            posMax = puntoA.position.y;
            if (velocidad < 0) velocidad *= -1;
        }
        else
        {
            posMin = puntoA.position.y;
            posMax = puntoB.position.y;
            if (velocidad > 0) velocidad *= -1;
        }
    }

    void Update()
    {
        if (!atacando) // Solo se mueve si no está atacando
        {
            // Si Phiro está a su derecha y está mirando a la izquierda
            if (transform.position.x < Phiro.position.x && transform.localScale.x < 0) flipEnemigo();

            // Si Phiro está a su izquierda y está mirando a la derecha
            if (transform.position.x > Phiro.position.x && transform.localScale.x > 0) flipEnemigo();

            if (!dentroPatrulla()) velocidad = -velocidad;

            desplazamientoEnemigo();
            i = 0;
        }

        else if (persiguiendo)  // Perseguir a Phiro
        {
            if (i++ >= 30) perseguir (Phiro.position.x, Phiro.position.y, absVel*3);
            if (i++ >= 120)
            {
                anim.SetTrigger("Regresa_Patrulla");
                persiguiendo = false;
            }              
        }

        else    // Regresar a patrulla
        {
            // Si Phiro está a su derecha y está mirando a la izquierda
            if (transform.position.x < Phiro.position.x && transform.localScale.x < 0) flipEnemigo();

            // Si Phiro está a su izquierda y está mirando a la derecha
            if (transform.position.x > Phiro.position.x && transform.localScale.x > 0) flipEnemigo();

            perseguir(patrullaX, patrullaY, absVel); 

            if (i++ >= 270 || 
                Mathf.Abs(transform.position.x - patrullaX) < absVel && Mathf.Abs(transform.position.y - patrullaY) < absVel) atacando = false;
        }
    }

    bool dentroPatrulla()    // Devuelve true si el enemigo no ha sobrepasado un punto patrulla
    {
        return (transform.position.y > posMin && transform.position.y < posMax);
    }

    void flipEnemigo() // Voltea la imagen del enemigo
    {
        // Si es positiva, mira a la derecha
        // Si es negativa, mira a la izquierda
        transform.localScale = new Vector3(-transform.localScale.x,
                                            transform.localScale.y,
                                            transform.localScale.z);
    }

    void desplazamientoEnemigo() // Se mueve en el eje Y
    {
        transform.position = new Vector3(transform.position.x,
                                         transform.position.y + velocidad,
                                         transform.position.z);
    }

    void perseguir (float objetivoX, float objetivoY, float vel)
    {
        float velX = 0;
        float velY = 0;

        if (Mathf.Abs (transform.position.x - objetivoX) > vel)
        {
            if (transform.position.x > objetivoX) velX = -vel;
            else velX = vel;
        }

        if (Mathf.Abs(transform.position.y - objetivoY) > vel)
        {
            if (transform.position.y > objetivoY) velY = -vel;
            else velY = vel;
        }

        transform.position = new Vector3(transform.position.x + velX,
                                 transform.position.y + velY,
                                 transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D other)    // Si toca a Phiro, activa ataque
    {
        if (other.tag == "Phiro" && atacando == false)
        {
            anim.SetTrigger("Entra_Phiro");
            atacando = true;
            persiguiendo = true;
        }
    }
}
