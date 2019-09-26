using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MainMenu_PONG_02: MonoBehaviour
{

    public List<GameObject> panelList;
    public Ball basicBall;
    public Slider basicSlider;
    public InputField nameInput;
    public Sprite iconMale;
    public Sprite iconFemale;
    Sprite icon;
    
    public void SelectIcon (int ID)
    {
        if (ID == 0)
            icon = iconMale;
        if (ID == 1)
            icon = iconFemale;
    }
    
    public void CreatePlayer ()
    {
        string name = nameInput.GetComponent <InputField> ().textComponent.text;
        
        Player newPlayer = new Player ();
        newPlayer.playerName = name;
        newPlayer.experience = 0;
        newPlayer.level = 0;
        newPlayer.ownedIcon = icon;
        newPlayer.ownedSliders.Add (basicSlider);
        newPlayer.ownwedBalls.Add (basicBall);
    }

    void ShowPlayer ()
    {

    }

    void Awake ()
    {
       PanelSwitch ("MainPanel");
    }

    public void PanelSwitch (string panelName)
    {
        foreach (GameObject panel in panelList)
        {
            if (panel.name == panelName)
            {
                panel.SetActive (true);
            }
            else
            {
                panel.SetActive (false);
            }
        }
    }

    public void Quit ()
    {
        Application.Quit ();
    }
}