using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Wave : MonoBehaviour {

    public float speed = .1f;
    Rigidbody2D rigid;

    // Use this for initialization
    void Start ()
    {
        rigid = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {

        Vector2 oldPos = rigid.position;
        oldPos.x += speed;

        if(oldPos.x >= 20)
        {
            Destroy(gameObject);
        }

        rigid.position = oldPos;
    }
}
