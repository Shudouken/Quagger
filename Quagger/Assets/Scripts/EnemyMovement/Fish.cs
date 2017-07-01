using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Fish : MonoBehaviour {

    Rigidbody2D rigid;
    float speed = 1;
    Vector3 spawnerPosition;
    FishSpawner fishSpawner;
    bool facingLeft = true;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Construct(float speed, Vector3 position, FishSpawner spawner, bool left)
    {
        this.speed = speed;
        spawnerPosition = position;
        fishSpawner = spawner;
        facingLeft = left;
    }

    void FixedUpdate () {
        Vector2 oldPos = rigid.position;
        if (facingLeft)
        {
            oldPos.x -= speed;
        }
        else
        {
            oldPos.x += speed;
        }
        rigid.position = oldPos;

    }

    private void OnBecameInvisible()
    {
        //Destroy(gameObject);
        //transform.position = spawnerPosition;
        fishSpawner.fishOutOfBoard.Add(gameObject);
    }
}
