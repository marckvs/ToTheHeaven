using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlatform : MonoBehaviour {

    [SerializeField]
    private float timeToDestroy;

    [SerializeField]
    private float timeToAppear;



    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "Phiro")
        {
            StartCoroutine(VisibilityFalse());
        }
    }

 
    IEnumerator VisibilityFalse()
    {

        yield return new WaitForSeconds(timeToDestroy);
        gameObject.SetActive(false);

    }

    



}
