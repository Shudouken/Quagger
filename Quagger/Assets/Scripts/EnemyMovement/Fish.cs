using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Fish : MonoBehaviour {

    Rigidbody2D rigid;
    float speed = 1;
    FishSpawner fishSpawner;
    RowSpawner spawner;
    bool facingLeft = true;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Construct(float speed, FishSpawner spawner, bool left)
    {
        this.speed = speed;
        fishSpawner = spawner;
        facingLeft = left;
    }

    public void Construct(float speed, RowSpawner spawner, bool left)
    {
        this.speed = speed;
        this.spawner = spawner;
        facingLeft = left;
    }

    void FixedUpdate () {
        Vector2 oldPos = rigid.position;
        if (facingLeft)
        {
            oldPos.x -= speed;
            if(oldPos.x <= -20)
            {
                if(fishSpawner != null)
                {
                    fishSpawner.fishOutOfBoard.Add(gameObject);
                }
                //fishSpawner.fishOutOfBoard.Add(gameObject);
                if (spawner != null)
                {
                    spawner.fishOutOfBoard.Add(gameObject);
                }
                gameObject.SetActive(false);
            }
        }
        else
        {
            oldPos.x += speed;
            if (oldPos.x >= 20)
            {
                if (fishSpawner != null)
                {
                    fishSpawner.fishOutOfBoard.Add(gameObject);
                }
                //fishSpawner.fishOutOfBoard.Add(gameObject);
                if (spawner != null)
                {
                    spawner.fishOutOfBoard.Add(gameObject);
                }
                gameObject.SetActive(false);
            }
        }
        rigid.position = oldPos;

    }
    /*
    private void OnBecameInvisible()
    {
        //Destroy(gameObject);
        //transform.position = spawnerPosition;
        fishSpawner.fishOutOfBoard.Add(gameObject);
    }
    **/
}
