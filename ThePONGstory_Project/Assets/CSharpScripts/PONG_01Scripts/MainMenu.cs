using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour 
{

    public InputField[] inputs;
    private int[] values = new int[14];
    private string[] prefs = new string[14] {"PlayerHealth", "EnemyHealth", 
											"MinionHealth", "PlayerSpeed",
											"EnemySpeed", "MinionSpeed",
											"WinningScore", "PlayerOffset",
											"BallLaunchX", "BallLaunchY",
											"BallPlayerMOD", "BallSelfMOD",
											"MusicVolume", "SoundVolume"};
    private string defaultSettings;

    public Toggle[] toggles;
    private string[] toggleprefs = new string[] { "Minion", "Music", "Sound" };

    public GameObject[] panels;
	
    public void DefaultSettings () 
    {
		Debug.Log ("Setting default values..");
		PlayerPrefs.SetString ("DefaultSettings", "True");
		
        values = new int[14] {50, 25, 10, 20, 20, 30, 25, 125, 80, 20, 50, 67, 50, 50};
		
        for (int i = 0; i < values.Length; i++ )
			PlayerPrefs.SetInt(prefs[i], values[i]);
		for (int i = 0; i < toggles.Length; i++) 
        {
			PlayerPrefs.SetString (toggleprefs[i], "enabled");
			toggles[i].isOn = true;
		}
		PlayerPrefs.Save ();
        ShowPrefs ();
	}

	public void SetCurrent () 
    {
		PlayerPrefs.SetString ("DefaultSettings", "False");
		for (int i = 0; i < inputs.Length; i++) 
        {
			values[i] = int.Parse (inputs[i].textComponent.text);
			PlayerPrefs.SetInt (prefs[i], values[i]);
		}
		for (int i = 0; i < toggles.Length; i++) 
        {
			if (toggles[i].isOn)
				PlayerPrefs.SetString (toggleprefs[i], "enabled");
			else 
				PlayerPrefs.SetString (toggleprefs[i], "disabled");
		}
		PlayerPrefs.Save ();
        ShowPrefs ();
	}

	void Start () 
    {
        PanelSwitch (0);
		
        if (!PlayerPrefs.HasKey ("DefaultSettings"))
			DefaultSettings ();
        ShowPrefs();
	}

    private void ShowPrefs ()
    {
        for (int i = 0; i < values.Length; i++)
        {
            values[i] = PlayerPrefs.GetInt (prefs[i]);
            inputs[i].text = values[i].ToString ();
        }

        for (int i = 0; i < toggles.Length; i++)
        {
            if (PlayerPrefs.GetString (toggleprefs[i]) == "enabled")
                toggles[i].isOn = true;
            else
                toggles[i].isOn = false;
        }
    }
    public void PanelSwitch(int panelID)
    {
        for (int i = 0; i < panels.Length; i++ )
        {
            if (i == panelID)
                panels[i].SetActive (true);
            else
                panels[i].SetActive (false);
        }
    }

	public void Play () 
    {
		Application.LoadLevel (1);
	}

	public void Quit () 
    {
		Application.Quit();
	}
}
