using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Fish : MonoBehaviour {

    Rigidbody2D rigid;
    float speed = 1;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate () {
        Vector2 oldPos = rigid.position;
        oldPos.x -= speed;
        rigid.position = oldPos;

    }
}
