using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlatform : MonoBehaviour {

    [SerializeField]
    private float timeToDestroy;

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
        this.gameObject.SetActive(false);
        Debug.Log("a");

    }

    



}
