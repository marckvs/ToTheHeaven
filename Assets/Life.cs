using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PhiroLifeState{
    Full,
    Mid,
    Dead
}

public class Life : MonoBehaviour {

    private Rigidbody2D _phiroRGD;
    private PhiroMovement pm;
    private float life = 2f;
   
    public PhiroLifeState lifestat;
    public float spawnCooldown = 2f;
    public Transform spawnPoint;
    public GameObject Phiro;

    void Start () {
        spawnPoint = GameObject.Find("Spawn").transform;
        pm = Phiro.GetComponent<PhiroMovement>();
        Phiro.transform.position = spawnPoint.position;
        lifestat = PhiroLifeState.Full;
        Phiro.gameObject.SetActive(true);
        pm.Reset();
	}

    private void Awake()
    {
        Phiro.transform.parent = null;
    }

    void Update () {
        if (pm.hit)
        {
            SubstractLife();
        }		
	}




    public void SubstractLife()
    {
        if(lifestat == PhiroLifeState.Full)
        {
            lifestat = PhiroLifeState.Mid;
        }
        else if(lifestat == PhiroLifeState.Mid)
        {
            lifestat = PhiroLifeState.Dead;
            StartCoroutine(RevivePhiro(spawnCooldown));
            Phiro.gameObject.SetActive(false);
        }
    }

    public IEnumerator RevivePhiro(float cooldown)
    {
        while ((cooldown -= Time.deltaTime)> 0)
        {
            yield return null;
        }
        Start();
        Debug.Log("Y VOLVIO A LA VIDA");
    }
}
