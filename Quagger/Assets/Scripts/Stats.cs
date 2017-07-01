using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {

    private Text texto;

    // Use this for initialization
    void Start () {
		texto = GetComponent<Text>();

        if (texto.text.Contains("Kill"))
            texto.text = "Kill Count: " + PlayerPrefs.GetInt("KillCount");
        else
            texto.text = "Used Continues: " + PlayerPrefs.GetInt("ContinueCount");
    }
	
}
