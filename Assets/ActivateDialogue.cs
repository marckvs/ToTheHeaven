﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDialogue : MonoBehaviour {

    [SerializeField]
    private GameObject g;

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Phiro")
        {
            g.SetActive(true);
        }
    }
}
