using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGM : MonoBehaviour {

    public AudioClip title;
    public AudioClip gameover;
    public AudioClip win;
    public AudioClip level_ground;
    public AudioClip level_sea;

    private AudioSource bgm;

	// Use this for initialization
	void Start () {
        bgm = GetComponent<AudioSource>();
        SceneManager.activeSceneChanged += changeSound;
	}

    private void changeSound(Scene previousScene, Scene newScene)
    {
        bgm.loop = true;
        StopAllCoroutines();

        if (newScene.name.Contains("2"))
            StartCoroutine(setClip(level_sea));
        else if (newScene.name.Contains("Title"))
            StartCoroutine(setClip(title));
        else if (newScene.name.Contains("over"))
        {
            setSong(gameover);
            bgm.loop = false;
        }
        else if (newScene.name.Contains("Win"))
        {
            bgm.volume = 0.15f;
            setSong(win);
            bgm.loop = false;
        }
        else if (newScene.name.Contains("Cutscene"))
            bgm.Pause();
        else
            StartCoroutine(setClip(level_ground));
    }

    IEnumerator setClip(AudioClip clip)
    {
        //wait for current song to loop
        if(bgm.isPlaying)
            yield return new WaitForSeconds(Mathf.Max(bgm.clip.length-bgm.time -0.08f, 0.0f));
        setSong(clip);
    }

    private void setSong(AudioClip clip)
    {
        bgm.Pause();
        Debug.Log("Switching bgm to " + clip.name);
        bgm.clip = clip;
        bgm.Play();
    }
}
