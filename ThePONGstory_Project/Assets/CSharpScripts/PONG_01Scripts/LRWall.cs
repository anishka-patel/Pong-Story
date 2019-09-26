using UnityEngine;
using System.Collections;

public class LRWall : MonoBehaviour 
{

	private AudioSource audioclip;

    public Sprite[] states;
    public SpriteRenderer spriteRenderer;

	private bool soundOn = true;
	
	void Start () 
    {
		audioclip = GetComponent <AudioSource> ();
        spriteRenderer = GetComponent <SpriteRenderer> ();
        if (PlayerPrefs.HasKey ("DefaultSettings"))
        {
            if (PlayerPrefs.GetString ("Sound") == "enabled")
            {
                soundOn = true;
                float volume = PlayerPrefs.GetInt ("SoundVolume") * .01f;
                audioclip.volume = volume;
            }
            else
                soundOn = false;
        }
	}

    void DisplayState(int stateID)
    {
        for (int i = 0; i < states.Length; i++)
            if (i == stateID)
                spriteRenderer.sprite = states[i];
    }

	void OnTriggerEnter2D (Collider2D collInfo) 
    {
		if (collInfo.name == "Ball")
		{
			string goalName = transform.name;
			GM.gM.StatScore (goalName);

            DisplayState(1);

			if (soundOn)
				audioclip.Play();

			collInfo.gameObject.SendMessage ("ResetBall");
		}
	}
	void OnTriggerExit2D (Collider2D collInfo) 
    {
        DisplayState (0);
	}
}
