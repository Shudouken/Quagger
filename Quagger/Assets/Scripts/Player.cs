using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Rigidbody2D rigid;
    public float speed = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            Vector2 oldPos = rigid.position;
            oldPos.x += Input.GetAxis("Horizontal") * speed;
            rigid.position = oldPos;
            //rigid.MovePosition(new Vector2(Input.GetAxis("Horizontal"), 0));
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            Vector2 oldPos = rigid.position;
            oldPos.y += Input.GetAxis("Vertical") * speed;
            rigid.position = oldPos;
        }
    }

}
