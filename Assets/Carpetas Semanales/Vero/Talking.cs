using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Talking : MonoBehaviour {
    //private Rigidbody2D _npcRGB;

    public Text hablar;
    //public Canvas canvas;
    private bool terminado = false;
    int contador = 0;
    private Queue<string> cola;
    public Image bocadillo;

    private string line;


    // Use this for initialization
    void Start () {

        //_npcRGB = GetComponent < Rigidbody2D >();
        hablar.text = "";
        //SetDialogo();
        cola = new Queue<string>();
        Dialogo(cola);
        bocadillo.enabled = false;
        hablar.enabled = false;
        //hablar.transform.position = new Vector3(-25, 15);
        
        //SetDialogo();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space) && contador > 0)
        {
            bocadillo.transform.position = hablar.transform.position;
            bocadillo.enabled = true;
            hablar.enabled = true;
            Imprime();
            

        }
        /*if(contador == 0)
        {
            bocadillo.enabled = false;
        }*/
   
        SetDialogo();

        

    }

    void SetDialogo()
    {
        if (contador == 0)
        {
            bocadillo.enabled = false;
            Dialogo(cola);
        }
   

    }

    private void Imprime()
    {
        hablar.text = cola.Dequeue();
        contador--;

        // cola.Dequeue();
    }

    void Dialogo(Queue<string> cola) {

        System.IO.StreamReader file = new System.IO.StreamReader(@"F:\Mis cosas\PROYECTO GIT\Assets\Carpetas Semanales\Vero\Historia.txt");

        while((line = file.ReadLine())!= null)
        {
            cola.Enqueue(line);
            contador++;
        }
        file.Close();
    }
}
