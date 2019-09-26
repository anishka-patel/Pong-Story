using UnityEngine;
using System.Collections;

public class AIControls : MonoBehaviour 
{

	public float botSpeed = 20f;

	public GameObject theBall;

	public Sprite[] states;
    private SpriteRenderer spriteRenderer;
	public GameObject minion;

	bool canSummon = true;

	void Start () 
    {
        if (PlayerPrefs.HasKey ("DefaultSettings"))
        {
            if (PlayerPrefs.GetString ("Minion") == "enabled")
            	canSummon = true;
            else
            	canSummon = false;
            botSpeed = PlayerPrefs.GetInt ("EnemySpeed") * 1.0f;
        }
        spriteRenderer = GetComponent <SpriteRenderer> ();
        DisplayState (0);
	}
	
	void FixedUpdate () 
    {
		Bot_Advanced () ;
	}

	void OnCollisionEnter2D (Collision2D collInfo)
    {
		int random = Random.Range (0, 2);

		if (collInfo.collider.tag == "Ball")
        {
			if (BallControl.isHit == true)
			{
				GM.gM.StatHealth (transform.name);
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
		canSummon = true;
	}

	void Bot_Basic () 
    {
				
		float posDiffY = theBall.transform.position.y - transform.position.y;
		float ballPos = theBall.transform.position.x;
		float playerPos = transform.position.x;
		float ballDir = theBall.GetComponent <Rigidbody2D> ().velocity.x;
		
		if (ballDir > 0 && ballPos < playerPos && ballPos > 0) 
        {	
			if ( posDiffY > 1 || posDiffY < -1)
            	transform.Translate (0.0f, 1 * (float) (posDiffY / Mathf.Abs (posDiffY)) * botSpeed * Time.fixedDeltaTime, 0.0f);
			else
				transform.Translate (0, posDiffY * botSpeed * Time.fixedDeltaTime, 0);
		}		
		else 
        	transform.Translate (0, -1 * transform.position.y * botSpeed * 0.33f * Time.fixedDeltaTime, 0);
	}

	void Bot_Advanced () 
    {
		if (GM.gM.p1Health - GM.gM.p2Health > 10  && canSummon)
        	StartCoroutine(SummonMinion());
		if (GM.gM.p1Health > GM.gM.p2Health && GM.gM.p1Score < GM.gM.wScore - 5) 
        {
			if (BallControl.isHit) 
				Bot_AvoidHit ();
			else 
            	Bot_Basic ();
		}
		else
			Bot_Basic();
	}
	
	void  Bot_AvoidHit () 
    {
		float posDiffY = theBall.transform.position.y - transform.position.y;
		float ballPos = theBall.transform.position.x;
		float playerPos = transform.position.x;
		float ballDir = theBall.GetComponent <Rigidbody2D>().velocity.x;

		if (ballDir > 0.0f && ballPos < playerPos && ballPos > 2.5f) 
        {	
			if ( posDiffY > 2.0f || posDiffY < -2.0f )
				transform.Translate (0, (1 / posDiffY) * Time.fixedDeltaTime, 0);
			else if (posDiffY != 0)
				transform.Translate (0.0f, -2.0f * (float)(posDiffY / Mathf.Abs (posDiffY)) * botSpeed  * Time.fixedDeltaTime, 0.0f);
			else
				transform.Translate (0.0f, -2.0f * Random.Range (-1, 1) * botSpeed  * Time.fixedDeltaTime, 0.0f);
		}
		else
			transform.Translate(0, -1 * transform.position.y * botSpeed * 0.33f * Time.fixedDeltaTime, 0);
	}

	IEnumerator SummonMinion () 
    {
		canSummon = false;
		yield return new WaitForSeconds (5);
		Vector3 posSummon = new Vector3 (transform.position.x - 2.5f, 0, 0);
		Instantiate(minion, posSummon, Quaternion.identity);
	}
}
