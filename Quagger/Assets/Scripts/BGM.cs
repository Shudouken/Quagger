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
        int pos = bgm.timeSamples;

        if (newScene.name.Contains("2"))
            setClip(level_sea, pos);
        else if (newScene.name.Contains("Title"))
            setClip(title, pos);
        else
            setClip(level_ground, pos);
    }

    private void setClip(AudioClip clip, int pos)
    {
        Debug.Log("Switching bgm to " + clip.name);
        //bgm.Pause();
        bgm.timeSamples = pos;
        bgm.clip = clip;
        bgm.Play();
    }
}
