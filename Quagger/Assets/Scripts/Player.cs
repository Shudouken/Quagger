using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public Rigidbody2D rigid;
    public Sprite standing;
    public Sprite swimming;
    public AudioClip damage_snd;

    private float speed = 0.15f;
    private Text timer;
    private Text hearts;
    private SpriteRenderer sprite;
    private AudioSource sfx;
    private bool in_water = false;
    private bool on_sand  = false;

    private bool invincible = false;
    private Vector2 start;

	// Use this for initialization
	void Start () {
        PlayerSingleton.getInstance().reset();
        start = rigid.position;
        timer = GameObject.Find("Timer").GetComponent<Text>();
        hearts = GameObject.Find("Hearts").GetComponent<Text>();
        sprite = GetComponent<SpriteRenderer>();
        sfx = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (PlayerSingleton.getInstance().hearts == 0)
        {
            PlayerPrefs.SetInt("Stage", SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene("Gameover");
        }

        PlayerSingleton.getInstance().incrementTimer(Time.deltaTime);
        timer.text = PlayerSingleton.getInstance().displayTime();
        hearts.text = new System.String('l', PlayerSingleton.getInstance().hearts);

        if (in_water)
            sprite.sprite = swimming;
        else
            sprite.sprite = standing;
    }

    public void takeDamage()
    {
        if (invincible)
            return;

        PlayerSingleton.getInstance().lose1Heart();
        sfx.PlayOneShot(damage_snd);
        StartCoroutine(respawn());
        StartCoroutine(flicker());
    }

    IEnumerator respawn()
    {
        invincible = true;
        rigid.position = start;
        rigid.rotation = 90;
        yield return new WaitForSeconds(2);
        invincible = false;
    }

    IEnumerator flicker()
    {
        for(int i = 0; i < 5; i++)
        { 
            yield return new WaitForSeconds(0.2f);
            sprite.enabled = false;
            yield return new WaitForSeconds(0.2f);
            sprite.enabled = true;
        }
    }

    public void setInWater(bool b)
    {
        in_water = b;
    }

    public void setOnSand(bool b)
    {
        on_sand = b;
    }

    private void FixedUpdate()
    {
        Vector2 move = new Vector2(0,0);

        move.x = Input.GetAxis("Horizontal") * speed;
        move.y = Input.GetAxis("Vertical") * speed;

        if (move != Vector2.zero)
        {
            float angle = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if(!sfx.isPlaying)
                sfx.PlayOneShot(sfx.clip);
        }

        rigid.position += move;
    }


}

public class PlayerSingleton
{

    public float time;
    public int hearts;

    private static PlayerSingleton instance;

    public static PlayerSingleton getInstance()
    {
        if (instance == null)
            instance = new PlayerSingleton();
        return instance;
    }

    private PlayerSingleton()
    {
        time = 0.0f;
        hearts = 3;
    }

    public void reset()
    {
        time = 0.0f;
        hearts = 3;
    }

    public void lose1Heart()
    {
        hearts = Mathf.Max(hearts - 1, 0);
    }

    // pass deltaTime here 
    public void incrementTimer(float amount)
    {
        time += amount;
    }

    public string displayTime()
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int hundredths = Mathf.FloorToInt((time * 100) % 100);

        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, hundredths);
    }

}
