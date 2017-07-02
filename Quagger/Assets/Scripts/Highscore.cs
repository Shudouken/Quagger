using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour {

    public Text _1;
    public Text _2;
    public Text _3;
    public Text _4;
    public Text _5;
    public Text _6;
    public Text _7;
    public Text _8;
    public Text _9;

    public InputField input;

    private bool cleared;
    private float time;
    private int kills;
    private int continues;
    private int rank;
    private string name;

	// Use this for initialization
	void Start () {
        if (!File.Exists("highscore"))
        {
            Debug.Log("Creating highscore file");
            StreamWriter temp = File.CreateText("highscore");
            temp.Close();
        }

        cleared = PlayerPrefs.GetInt("Cleared") == 1;

        if (cleared)
        {
            time = PlayerPrefs.GetFloat("Time");
            kills = PlayerPrefs.GetInt("KillCount");
            continues = PlayerPrefs.GetInt("ContinueCount");
            rank = calcuateRank();

            if (rank != 0)
            { 
                enterHighscore();
                return;
            }
        }

        showLeaderboard();
    }

    void Update()
    {
        if (input.isFocused)
        {
            if (Input.GetKeyUp(KeyCode.Return))
            {
                input.DeactivateInputField();
                name = input.text;
                input.text = "ABC";
                input.enabled = false;

                writeHighscore();
                showLeaderboard();
            }
        }
    }

    //return 1-9 if in top 9, else 0
    private int calcuateRank()
    {
        return 1;
    }

    private void enterHighscore()
    {
        input.enabled = true;
        input.ActivateInputField();
    }

    private void showLeaderboard()
    {
        StreamReader file = File.OpenText("highscore");
        int count = 0;
        string l = file.ReadLine();

        while (count++ <= 9)
        {
            getLabel(count).text = l;

            l = file.ReadLine();
        }
        file.Close();
    }

    private void writeHighscore()
    {
        StreamReader file = File.OpenText("highscore");
        int count = 0;
        string l = file.ReadLine();

        while (count++ <= 9)
        {
            if (count == rank)
                getLabel(count).text = scoreToText();
            else
            { 
                getLabel(count).text = l;

                l = file.ReadLine();
            }
        }
        file.Close();

        StreamWriter filew = File.CreateText("highscore");

        filew.WriteLine(_1);
        filew.WriteLine(_2);
        filew.WriteLine(_3);
        filew.WriteLine(_4);
        filew.WriteLine(_5);
        filew.WriteLine(_6);
        filew.WriteLine(_7);
        filew.WriteLine(_8);
        filew.WriteLine(_9);

        filew.Close();
    }

    private Text getLabel(int count)
    {
        switch (count)
        {
            case 1:  return _1;
            case 2:  return _2;
            case 3:  return _3;
            case 4:  return _4;
            case 5:  return _5;
            case 6:  return _6;
            case 7:  return _7;
            case 8:  return _8;
            default: return _9;
        }
    }

    public string scoreToText()
    {
        //1.ASH  \  99:99:99  \    999     \    999

        return "" + rank + "." + name + "  \\  " + displayTime(time) + "  \\    " +
            kills.ToString().PadLeft(3, '0') + "     \\    " + continues.ToString().PadLeft(3, '0');
    }

    public string displayTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int hundredths = Mathf.FloorToInt((time * 100) % 100);

        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, hundredths);
    }
}
