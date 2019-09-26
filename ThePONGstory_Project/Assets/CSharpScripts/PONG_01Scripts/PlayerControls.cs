using UnityEngine;
using System.Collections;

public class PlayerControls : MonoBehaviour 
{

	public KeyCode moveUp;
	public KeyCode moveDown;

	public TouchInput down;
	public TouchInput up;

	public float playerSpeed = 20f;
	
	public Sprite[] states;
    public SpriteRenderer spriteRenderer;

	void Start () 
    {
        spriteRenderer = GetComponent <SpriteRenderer> ();
        
		if (PlayerPrefs.HasKey ("DefaultSettings"))
        	playerSpeed = PlayerPrefs.GetInt ("PlayerSpeed") * 1.0f;
        DisplayState (0);
	}
	
	void Update () 
    {
		Player() ;
	}

	void OnCollisionEnter2D (Collision2D collInfo)
    {
		int random = Random.Range (0, 2);

		if (collInfo.collider.tag == "Ball")
        {
			if (BallControl.isHit == true)
			{
				GM.gM.StatHealth(transform.name);
				collInfo.gameObject.SendMessage ("PlayerHit");
                DisplayState (2);
			}
			else if(random == 1) 
            {
				collInfo.gameObject.SendMessage ("BallHit");
				DisplayState (1);
			}
			else 
            {
				collInfo.gameObject.SendMessage ("NormalHit");
                DisplayState (1);
			}
		}
		StartCoroutine (SetPlayerNormal ());
	}

    void DisplayState (int stateID)
    {
        for (int i = 0; i < states.Length; i++)
        	if (i == stateID)
            	spriteRenderer.sprite = states[i];
        
    }

	IEnumerator SetPlayerNormal () 
    {
		yield return new WaitForSeconds (1);
        DisplayState (0);
	}
	
	public void ResetPlayer () 
    {
		transform.position = new Vector2 (transform.position.x, 0f);
	}

	void Player () 
    {
		if /*(Input.GetKey (moveUp))*/ (up.IfTouched()) 
			GetComponent <Rigidbody2D> ().velocity = new Vector2 (0f, playerSpeed * 1);
		else if /*(Input.GetKey (moveDown))*/ (down.IfTouched()) 
			GetComponent <Rigidbody2D> ().velocity = new Vector2 (0f, playerSpeed * -1);
		else 
        	GetComponent <Rigidbody2D> ().velocity = Vector2.zero;
	}
}
