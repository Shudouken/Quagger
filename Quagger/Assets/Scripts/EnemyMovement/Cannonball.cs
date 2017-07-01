using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Cannonball : MonoBehaviour {

    Rigidbody2D rigid;
    //float speed = 1;

    float verticalDirection = 0;
    float horizontalDirection = 0;
    Vector3 cannonPosition;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Construct(float vertical, float horizontal, Vector3 position)
    {
        verticalDirection = vertical;
        horizontalDirection = horizontal;
        cannonPosition = position;
    }

    void FixedUpdate()
    {
        Vector2 oldPos = rigid.position;
        oldPos.x += horizontalDirection;
        oldPos.y += verticalDirection;
        rigid.position = oldPos;

    }

    private void OnBecameInvisible()
    {
        //Destroy(gameObject);
        transform.position = cannonPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        collision.GetComponent<Player>().takeDamage();
    }
}
