using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateZipCollider : MonoBehaviour {

    [SerializeField]
    private GameObject g;

    private void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.tag == "Phiro")
        {
            g.SetActive(true);
        }
    }
}
