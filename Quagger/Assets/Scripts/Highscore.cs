using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour {

    public Text _0;
    public Text _1;
    public Text _2;
    public Text _3;
    public Text _4;
    public Text _5;
    public Text _6;
    public Text _7;
    public Text _8;
    public Text _9;

    public GameObject inputui;
    public InputField input;
    public GameObject start;

    private bool cleared;
    private float time;
    private int kills;
    private int continues;
    private int rank;
    private string name;

    private static string highfile = "highscore.txt";

	// Use this for initialization
	void Start () {
        if (!File.Exists(highfile))
        {
            Debug.Log("Creating highscore file");
            StreamWriter temp = File.CreateText(highfile);
            for (int i = 1; i <= 9; i++)
                temp.WriteLine(scoreToText(i,"___",0,0,0));
            temp.Close();
        }

        cleared = PlayerPrefs.GetInt("Cleared") == 1;

        if (cleared)
        {
            start.SetActive(false);
            PlayerPrefs.SetInt("Cleared",0);
            time = PlayerPrefs.GetFloat("Time");
            kills = PlayerPrefs.GetInt("KillCount");
            continues = PlayerPrefs.GetInt("ContinueCount");
            rank = calcuateRank();

            if (rank != 0)
            {
                name = "you";
                _0.text = scoreToText();
                enterHighscore();
                return;
            }
        }

        showLeaderboard();
    }

    void Update()
    {
        if (cleared && Input.GetKeyUp(KeyCode.Return))
        {
            name = input.text;
            if (name == "")
                name = "___";
            else
                name = name.PadRight(3, ' ');
            Debug.Log(name);
            input.text = "ABC";
            input.DeactivateInputField();
            input.enabled = false;
            inputui.SetActive(false);

            writeHighscore();
            showLeaderboard();
            cleared = false;
            start.SetActive(true);
        }
    }

    //return 1-9 if in top 9, else 0
    private int calcuateRank()
    {
        StreamReader file = File.OpenText(highfile);
        int count = 0;
        string l = file.ReadLine();
        int rank = 1;

        bool newHighscore = false;
        bool inHighscore = false;

        while (count++ < 9)
        {
            string score = file.ReadLine();
            if (score == null)
            {
                inHighscore = true;
                break;
            }
            rank++;

            string rankTime = score.Split(new string[] { "\\" }, StringSplitOptions.None)[1];
            float compareTime = getSecondsFromString(rankTime);
            
            if (time < compareTime)
            {
                newHighscore = true;
                break;
            }

        }
        file.Close();

        if (inHighscore)
        {
            return rank;
        }
        if (!newHighscore)
        {
            rank = 0;
        }
        return rank;
    }

    private void enterHighscore()
    {
        inputui.SetActive(true);
        input.enabled = true;
        input.ActivateInputField();
        input.interactable = true;
    }

    private void showLeaderboard()
    {
        StreamReader file = File.OpenText(highfile);
        int count = 0;
        string l = file.ReadLine();

        while (count++ < 9)
        {
            if (l == null || l.Length == 0)
                break;
            getLabel(count).text = l;

            l = file.ReadLine();
        }
        file.Close();
    }

    private void writeHighscore()
    {
        StreamReader file = File.OpenText(highfile);
        int count = 0;
        string l = file.ReadLine();

        while (count++ < 9)
        {
            if (count == rank)
            { 
                getLabel(count).text = scoreToText();
                /*if (l == null || l.Length == 0)
                    break;
                count++;
                getLabel(count).text = "" + count + l.Substring(1);*/
            }
            else
            {
                if (l == null || l.Length == 0)
                    break;
                getLabel(count).text = "" + count + l.Substring(1);

                l = file.ReadLine();
            }
        }
        file.Close();

        StreamWriter filew = File.CreateText(highfile);

        filew.WriteLine(_1.text);
        filew.WriteLine(_2.text);
        filew.WriteLine(_3.text);
        filew.WriteLine(_4.text);
        filew.WriteLine(_5.text);
        filew.WriteLine(_6.text);
        filew.WriteLine(_7.text);
        filew.WriteLine(_8.text);
        filew.WriteLine(_9.text);

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

    public string scoreToText(int r, string n, float t, int k, int c)
    {
        //1.ASH  \  99:99:99  \    999     \    999

        return "" + r + "." + n + "  \\  " + displayTime(t) + "  \\    " +
            k.ToString().PadLeft(3, '0') + "     \\    " + c.ToString().PadLeft(3, '0');
    }

    public string displayTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int hundredths = Mathf.FloorToInt((time * 100) % 100);

        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, hundredths);
    }

    public float getSecondsFromString(string time)
    {
        float timeInHundreths = 0;
        string[] timeParts = time.Split(':');
        
        timeInHundreths = float.Parse(timeParts[0]) * 60;
        timeInHundreths += float.Parse(timeParts[1]);
        timeInHundreths += float.Parse(timeParts[2]) / 100;
        
        return timeInHundreths;
    }
}
