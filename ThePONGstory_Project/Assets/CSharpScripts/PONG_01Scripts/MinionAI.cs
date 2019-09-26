using UnityEngine;
using System.Collections;

public class MinionAI : MonoBehaviour 
{

	public float botSpeed = 30f;

	private GameObject theBall;

    public SpriteRenderer spriteRenderer;
    public Sprite[] states;

    public GameObject minion;
	
	void Start () 
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
     
        if (PlayerPrefs.HasKey ("DefaultSettings"))
			botSpeed = PlayerPrefs.GetInt ("MinionSpeed") * 1.0f;

		if (theBall == null) 
			theBall = GameObject.FindGameObjectWithTag ("Ball");
		DisplayState (0);
	}
	
	
	void FixedUpdate () 
    {
		Bot_Minion () ;
	}

	void OnCollisionEnter2D (Collision2D collInfo)
    {
		int random = Random.Range (0, 2);
		if (collInfo.collider.tag == "Ball")
        {
			
			if (BallControl.isHit == true)
            {
				GM.gM.StatHealth (transform.name);
				collInfo.gameObject.SendMessage("PlayerHit");
                DisplayState (2);
			}
			else if(random == 1) 
            {
				collInfo.gameObject.SendMessage("BallHit");
				DisplayState (1);
			}
			else 
            {
				collInfo.gameObject.SendMessage("NormalHit");
				DisplayState (1);
			}
		}
		StartCoroutine (SetPlayerNormal ());
	}
    
	void DisplayState (int stateID)
    {
        for (int i = 1; i < states.Length; i++)
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
		transform.position = new Vector2 ( transform.position.x, 0f);
		Destroy (this.gameObject);

	}

	void Bot_Minion () 
    {
		if (GM.gM.minionHealth <= 0)
			Destroy(this.gameObject);
		
		float posDiffY = theBall.transform.position.y - transform.position.y;
		float ballPos = theBall.transform.position.x;
		float playerPos = transform.position.x;
		float ballDir = theBall.GetComponent <Rigidbody2D>().velocity.x;
		
		if (ballDir > 0 && ballPos < playerPos ) 
        {	
			if ( posDiffY > 0.5f )
				transform.Translate (0, 0.5f *botSpeed * Time.fixedDeltaTime, 0);
			else if (posDiffY < -0.5f)
				transform.Translate (0, -0.5f * botSpeed * Time.fixedDeltaTime, 0);
			else
				transform.Translate (0, posDiffY * botSpeed * Time.fixedDeltaTime, 0);
		}
		else if (ballPos > playerPos + 0.25f && Mathf.Abs(posDiffY) < 2f && (ballPos < playerPos + 2.75f))
        {
			if (posDiffY != 0)
				transform.Translate (0.0f, -2.0f * (float)(posDiffY / Mathf.Abs (posDiffY)) * botSpeed  * Time.fixedDeltaTime, 0.0f);
			else
				transform.Translate (0.0f, -2.0f * Random.Range (-1, 1) * botSpeed  * Time.fixedDeltaTime, 0.0f);
		}
		else
			transform.Translate(0, -1 * transform.position.y * Time.fixedDeltaTime * botSpeed * 0.25f, 0);
	}
}
