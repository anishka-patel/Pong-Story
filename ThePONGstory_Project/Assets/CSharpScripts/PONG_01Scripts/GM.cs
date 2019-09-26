using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class GM : MonoBehaviour 
{

	public int p1Score = 0;
	public int p2Score = 0;
	public int p1Level = 1;
	public int p1Exp = 0;
	public int p2Level = 1;
	public int p2Exp = 0;
	public int levelUpExp;
	public int p1Health = 50;
	public int p2Health = 25;
	public int minionHealth = 10;

	public int wScore = 25;
	private bool winnerFound = false;
	private string isWinner;
    private bool playSound = true;

	public Text stat1;
	public Text stat2;
	public Text stat3;
	public Text stat4;
	public Text stat5;
	public Text stat6;
	public Text stat7;
	public Text stat8;

	public GameObject theBall;
	public GameObject player01;
	public GameObject player02;

	public static GM gM;

	void Awake () 
    {		

		if (gM == null) 
        {
			gM = this;
		}
		else if (gM != this) 
        {
			Destroy (this.gameObject);
		}

	}

	void Start () 
    {
        if (PlayerPrefs.HasKey("DefaultSettings"))
        {
            p1Health = PlayerPrefs.GetInt("PlayerHealth");
            p2Health = PlayerPrefs.GetInt("EnemyHealth");
            minionHealth = PlayerPrefs.GetInt("MinionHealth");
            wScore = PlayerPrefs.GetInt("WinningScore");
            if (PlayerPrefs.GetString("Music") == "enabled")
            {
                float volume = PlayerPrefs.GetInt("MusicVolume") * 0.01f;
                GetComponent<AudioSource>().volume = volume;
                playSound = true;
            }
            else
            {
                playSound = false;
            }

            StartCoroutine(AudioPlay());
        }
		

		levelUpExp = 150;
	}

	void Update () 
    {
		stat1.text = "Health\n" + p1Health.ToString();
		stat2.text = "Health\n" + p2Health.ToString();
		stat3.text = "Score\n" + p1Score.ToString();
		stat4.text = "Score\n" + p2Score.ToString();
		stat5.text = isWinner;
		stat6.text = "Level: " + p1Level.ToString () + "\n" + "Exp: " + p1Exp.ToString () +"/" + levelUpExp.ToString (); 
		stat7.text = "Level: " + p2Level.ToString () + "\n" + "Exp: " + p2Exp.ToString () +"/" + levelUpExp.ToString ();
		if (PlayerPrefs.GetString("Minion") == "enabled") 
        {
			stat8.text = "Minion\n" + minionHealth.ToString ();
		}
		if (winnerFound) 
        {
			StartCoroutine(ShowWinner ());
		}
	}
	
	public void StatScore (string name) 
    {
		switch (name) 
        {
		case "leftWall" :
			p2Score += 1;
			p2Exp += 5;
			break;
		case "rightWall" :
			p1Score += 1;
			p1Exp += 5;
			break;
		default : 
			break;
		}
		LevelUpCheck ();
		WinnerCheck ();
	}

	public void StatHealth (string name) 
    {
		switch (name) 
        {
		case "Player01" :
			p1Health -= 1;
			p2Exp += 10;
			break;
		case "Player02" :
			p2Health -= 1;
			p1Exp += 10;
			break;
		case "Minion(Clone)" :
			minionHealth -= 1;
			p1Exp += 5;
			break;
		default :
			break;
		}
		LevelUpCheck ();
		WinnerCheck ();
	}

	public void LevelUpCheck () 
    {
		if (p1Exp >= levelUpExp) 
        {
			p1Level += 1;
			p1Exp = 0;
			levelUpExp = p1Level * 150;
		}
		if (p2Exp >= levelUpExp) 
        {
			p2Level += 1;
			p2Exp = 0;
			levelUpExp = p1Level * 150;
		}
	}

	public void WinnerCheck () 
    {

		if (p1Health<= 0 || p2Health <= 0) 
        {
			if (p1Health <= 0 && p2Health > 0) 
            {
				SetWinner ("Player 2");
			}
			else if (p2Health <= 0 && p1Health > 0 ) 
            {
				SetWinner ("Player 1");
			}
			else if (p1Score > p2Score) 
            {
				SetWinner ("Player 1");
			}
			else if (p2Score > p1Score) 
            {
				SetWinner ("Player 2");
			}
			else {
				IsTied ();
			} 
		}
		if (p1Score == wScore || p2Score == wScore)
        {
			if (p1Score == wScore && p2Score < p1Score) 
            {
				SetWinner ("Player 1");
			}
			else if (p2Score == wScore && p1Score < p2Score) 
            {
				SetWinner ("Player 2");
			}
			else if (p1Health > p2Health) 
            {
				SetWinner ("Player 1");
			}
			else if (p2Health < p1Health) 
            {
				SetWinner ("Player 2");
			}
			else {
				IsTied();
			}
		}
	}

	public void ResetGame () 
    {
		p1Health = 50;
		p2Health = 25;
		minionHealth = 10;
		p1Score = 0;
		p2Score = 0;
		
		winnerFound = false;
		isWinner = "";

	}

	public void SetWinner (string winner) 
    {

		winnerFound = true;
		isWinner = winner + " wins!";
	}

	public void IsTied () 
    {

		winnerFound = true;
		isWinner = "It's a tie!";
	}

	public IEnumerator AudioPlay () 
    {
        if (playSound)
        {
            yield return new WaitForSeconds(2);
            GetComponent<AudioSource>().Play();
            GetComponent<AudioSource>().loop = true;
        }
	}
	
	public IEnumerator ShowWinner () 
    {

		stat5.text = isWinner;
		BallControl.stopBall = true;
		yield return new WaitForSeconds (3);
		ResetGame () ;
	}

	public void ResetButton () 
    {
		ResetGame ();
		BallControl.stopBall = false;
		theBall.gameObject.SendMessage("ResetBall");
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		foreach ( GameObject player in players) {
			player.gameObject.SendMessage("ResetPlayer",SendMessageOptions.DontRequireReceiver);
		}
		Application.LoadLevel(2);
	}

	public void Back () 
    {

		Application.LoadLevel (0);
	}
}
