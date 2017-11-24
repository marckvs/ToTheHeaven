using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTestMovementX : MonoBehaviour {
    private Rigidbody2D _Rigid;
    [Range(1,1000)]
    public float velocityX;
	// Use this for initialization
	void Start () {
        _Rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        _Rigid.velocity = new Vector2(velocityX * Time.fixedDeltaTime, _Rigid.velocity.y);
	}
}
