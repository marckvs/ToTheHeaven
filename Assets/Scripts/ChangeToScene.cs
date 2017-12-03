using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToScene : MonoBehaviour {

    public int sceneNumber;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Phiro")
        {
            SceneManager.LoadScene(sceneNumber);
        }
    }
}
