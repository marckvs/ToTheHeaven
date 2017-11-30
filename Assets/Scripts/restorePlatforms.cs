using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restorePlatforms : MonoBehaviour {


    [SerializeField]
    private GameObject P1,P2,P3,P4,P5,P6,P7,P8,P9,P10,P11/*,P12,P13*/;

    private bool p1_invisible = false;
    private bool p2_invisible = false;
    private bool p3_invisible = false;
    private bool p4_invisible = false;
    private bool p5_invisible = false;
    private bool p6_invisible = false;
    private bool p7_invisible = false;
    private bool p8_invisible = false;
    private bool p9_invisible = false;
    private bool p10_invisible = false;
    private bool p11_invisible = false;
    /*private bool p12_invisible = false;
    private bool p13_invisible = false;*/

    [SerializeField]
    private float timeToAppear;

    void Update()
    {
        if (P1.activeInHierarchy == false && p1_invisible == false) {
            Debug.Log("no activo");
            p1_invisible = true;
            StartCoroutine(visibilityTrue());

        }
        if (P2.activeInHierarchy == false && p2_invisible == false)
         {
             Debug.Log("no activo");
             p2_invisible = true;
             StartCoroutine(visibilityTrue2());
         }
         if (P3.activeInHierarchy == false && p3_invisible == false)
         {
             Debug.Log("no activo");
             p3_invisible = true;
             StartCoroutine(visibilityTrue3());
         }
         if (P4.activeInHierarchy == false && p4_invisible == false)
         {
             Debug.Log("no activo");
             p4_invisible = true;
             StartCoroutine(visibilityTrue4());
         }
         if (P5.activeInHierarchy == false && p5_invisible == false)
         {
             Debug.Log("no activo");
             p5_invisible = true;
             StartCoroutine(visibilityTrue5());
         }
         if (P6.activeInHierarchy == false && p6_invisible == false)
         {
             Debug.Log("no activo");
             p6_invisible = true;
             StartCoroutine(visibilityTrue6());
         }
         if (P7.activeInHierarchy == false && p7_invisible == false)
         {
             Debug.Log("no activo");
             p7_invisible = true;
             StartCoroutine(visibilityTrue7());
         }
         if (P8.activeInHierarchy == false && p8_invisible == false)
         {
             Debug.Log("no activo");
             p8_invisible = true;
             StartCoroutine(visibilityTrue8());
         }
         if (P9.activeInHierarchy == false && p9_invisible == false)
         {
             Debug.Log("no activo");
             p9_invisible = true;
             StartCoroutine(visibilityTrue9());
         }
         if (P10.activeInHierarchy == false && p10_invisible == false)
         {
             Debug.Log("no activo");
             p10_invisible = true;
             StartCoroutine(visibilityTrue10());
         }
         if (P11.activeInHierarchy == false && p11_invisible == false)
         {
             Debug.Log("no activo");
             p11_invisible = true;
             StartCoroutine(visibilityTrue11());
         }
         /*if (P12.activeInHierarchy == false && p12_invisible == false)
         {
             Debug.Log("no activo");
             p12_invisible = true;
             StartCoroutine(visibilityTrue12());
         }
         if (P13.activeInHierarchy == false && p13_invisible == false)
         {
             Debug.Log("no activo");
             p13_invisible = true;
             StartCoroutine(visibilityTrue13());
         }*/
    }

    IEnumerator visibilityTrue()
    {
        yield return new WaitForSeconds(timeToAppear);
        P1.SetActive(true);
        p1_invisible = false;
    }

    IEnumerator visibilityTrue2()
    {
        yield return new WaitForSeconds(timeToAppear);
        P2.SetActive(true);
        p2_invisible = false;
    }

    IEnumerator visibilityTrue3()
    {
        yield return new WaitForSeconds(timeToAppear);
        P3.SetActive(true);
        p3_invisible = false;
    }

    IEnumerator visibilityTrue4()
    {
        yield return new WaitForSeconds(timeToAppear);
        P4.SetActive(true);
        p4_invisible = false;
    }
    IEnumerator visibilityTrue5()
    {
        yield return new WaitForSeconds(timeToAppear);
        P5.SetActive(true);
        p5_invisible = false;
    }
    IEnumerator visibilityTrue6()
    {
        yield return new WaitForSeconds(timeToAppear);
        P6.SetActive(true);
        p6_invisible = false;
    }
    IEnumerator visibilityTrue7()
    {
        yield return new WaitForSeconds(timeToAppear);
        P7.SetActive(true);
        p7_invisible = false;
    }
    IEnumerator visibilityTrue8()
    {
        yield return new WaitForSeconds(timeToAppear);
        P8.SetActive(true);
        p8_invisible = false;
    }

    IEnumerator visibilityTrue9()
    {
        yield return new WaitForSeconds(timeToAppear);
        P9.SetActive(true);
        p9_invisible = false;
    }
    IEnumerator visibilityTrue10()
    {
        yield return new WaitForSeconds(timeToAppear);
        P10.SetActive(true);
        p10_invisible = false;
    }
    IEnumerator visibilityTrue11()
    {
        yield return new WaitForSeconds(timeToAppear);
        P11.SetActive(true);
        p11_invisible = false;
    }
   /* IEnumerator visibilityTrue12()
    {
        yield return new WaitForSeconds(timeToAppear);
        P12.SetActive(true);
        p12_invisible = false;
    }
    IEnumerator visibilityTrue13()
    {
        yield return new WaitForSeconds(timeToAppear);
        P13.SetActive(true);
        p13_invisible = false;
    }*/












}
