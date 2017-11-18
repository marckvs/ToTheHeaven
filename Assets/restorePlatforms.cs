using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restorePlatforms : MonoBehaviour {

    GameObject P1;

    private bool p1_visible = false;

    [SerializeField]
    private float timeToAppear;

    void Start()
    {
        P1 = GameObject.Find("p_tipo_11_1");
    }

    void Update()
    {
        if (P1.activeInHierarchy == false && p1_visible == false) {
            Debug.Log("no activo");
            p1_visible = true;
            StartCoroutine(visibilityTrue());
        }
    }

    IEnumerator visibilityTrue()
    {
        yield return new WaitForSeconds(timeToAppear);
        P1.SetActive(true);
        p1_visible = true;
    }
        
}
