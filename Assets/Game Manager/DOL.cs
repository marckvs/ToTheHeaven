using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;

public class DOL : MonoBehaviour {

    void Awake()
    {
        GameObject.DontDestroyOnLoad(this);
    }
}
