using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode
{
    idle,
    playing, 
    levelEnd
}

public class MissionDemotilion : MonoBehaviour
{
    static private MissionDemotilion S;

    [Header("Set in Inspector")]
    public Text uitLevel;
    public Text uitShot;
    public Text uitButton;
    public Vector3 castlePos;
    public GameObject[] castles;

    [Header("Set Dynamically")]
    public int level;
    public int levelMax;
    public int shotsTalen;
    public GameObject castle;
    public GameMode mode = GameMode.idle;
    public string showing = "Showing Slingshot";

    public void Start()
    {
        S = this;
        levelMax = castles.Length;
        StartLevel();
    }

    public void StartLevel() 
    {
        if (castle != null)
        {
            Destroy(castle);
        }

        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (var item in gos)
        {
            Destroy(item);
        }

        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;
        shotsTalen = 0;

        SwitchView("Show Both");
        ProjectLine.S.Clear();

        Goal.goalMet = false;

        UpdateGUI();

        mode = GameMode.playing;
    }

    public void UpdateGUI() 
    {
        uitLevel.text = "Level: " + (level + 1) + "of " + levelMax;
        uitShot.text = "Shots Taken: " + shotsTalen;
    }

    public void Update()
    {
        UpdateGUI();

        if ((mode == GameMode.playing)&&Goal.goalMet)
        {
            mode = GameMode.levelEnd;

            SwitchView("Show Both");
            Invoke("NextLevel", 2f);
        }
    }

    public void NextLevel() 
    {
        level++;
        if (level == levelMax)
        {
            level = 0;
        }
        StartLevel();
    }

    public void SwitchView(string eView = "") 
    {
        if (eView == "")
        {
            eView = uitButton.text;
        }
        showing = eView;
        switch (showing) { 
            case "Show Slingshot":
                FollowCam.POI = null;
                uitButton.text = "Show Castle";
                break;

            case "Show Castle":
                FollowCam.POI = S.castle;
                uitButton.text = "Show Both";
                break;

            case "Show Both":
                FollowCam.POI = GameObject.Find("ViewBoth");
                uitButton.text = "Show Slingshot";
                break;
        }
    }

    public static void ShotFired() 
    {
        S.shotsTalen++;
    }
}
