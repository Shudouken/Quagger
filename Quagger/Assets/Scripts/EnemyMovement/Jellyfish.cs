using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jellyfish : MonoBehaviour {

    Rigidbody2D rigid;
    float speed = 1;
    Vector3 spawnerPosition;
    JellyfishSpawner fishSpawner;
    bool facingLeft = true;
    bool floating = true;

    Vector2 floatingEndPosition = Vector2.one;
    Vector2 swimmingEndPosition = Vector2.one;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        floatingEndPosition = new Vector2(transform.position.x, transform.position.y - 1.5f);
    }

    public void Construct(float speed, Vector3 position, JellyfishSpawner spawner, bool left)
    {
        this.speed = speed;
        spawnerPosition = position;
        fishSpawner = spawner;
        facingLeft = left;
    }

    void FixedUpdate()
    {
        float factor = facingLeft ? -3f : 3f;

        float step = speed * Time.deltaTime;

        if (floating)
        {
            rigid.position = Vector3.MoveTowards(rigid.position, floatingEndPosition, step);

            if (rigid.position == floatingEndPosition)
            {
                floating = false;
                swimmingEndPosition = new Vector3(rigid.position.x + factor, rigid.position.y + 1.5f);

            }
        }
        else
        {
            rigid.position = Vector3.MoveTowards(rigid.position, swimmingEndPosition, step * 3);
            /*
            if (facingLeft && rigid.position.x <= -20)
            {
                //fishSpawner.fishOutOfBoard.Add(gameObject);
                //gameObject.SetActive(false);
                rigid.position = new Vector2(20, rigid.position.y);
            }
            if (!facingLeft && rigid.position.x >= 20)
            {
                //fishSpawner.fishOutOfBoard.Add(gameObject);
                //gameObject.SetActive(false);
                rigid.position = new Vector2(-20, rigid.position.y);
            }
            */
            if (rigid.position == swimmingEndPosition)
            {
                if (facingLeft && rigid.position.x <= -20)
                {
                    //fishSpawner.fishOutOfBoard.Add(gameObject);
                    //gameObject.SetActive(false);
                    rigid.position = new Vector2(20, rigid.position.y);
                }
                if (!facingLeft && rigid.position.x >= 20)
                {
                    //fishSpawner.fishOutOfBoard.Add(gameObject);
                    //gameObject.SetActive(false);
                    rigid.position = new Vector2(-20, rigid.position.y);
                }
                floating = true;
                floatingEndPosition = new Vector3(rigid.position.x, rigid.position.y - 1.5f);

            }
        }

    }
}
