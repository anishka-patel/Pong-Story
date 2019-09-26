using UnityEngine;
using System.Collections;
using UnityEditor;

public class BallControl : MonoBehaviour
{
    public int xForce = 80;
    public int yForce = 20;
    public float ballMOD = 0.85f;
    public float playerMOD = 0.50f;

    public static bool stopBall = true;
    public static bool isHit = false;

    private bool checkBall = false;
    private bool soundOn = true;

    private AudioSource audioclip;

    public Sprite[] states;
    
	private Color32[][] colors;
    private SpriteRenderer spriteRenderer;
	private TrailRenderer trailRenderer;
	private SerializedObject sObject;
    
	public GameObject readyButton;

    void Start()
    {
		CreateColors ();
		audioclip = GetComponent <AudioSource> ();
        spriteRenderer = GetComponent <SpriteRenderer> ();
		trailRenderer = GetComponent <TrailRenderer> ();
		sObject = new SerializedObject (trailRenderer);
//		SerializedProperty it = sObject.GetIterator();
//		
//		while (it.Next(true))
//			Debug.Log(it.propertyPath);
        

        if (PlayerPrefs.HasKey ("DefaultSettings"))
        {
            xForce = PlayerPrefs.GetInt ("BallLaunchX");
            yForce = PlayerPrefs.GetInt ("BallLaunckY");
            ballMOD = PlayerPrefs.GetInt ("BallSelfMOD") * 0.01f;
            playerMOD = PlayerPrefs.GetInt ("BallPlayerMOD") * 0.01f;

            if (PlayerPrefs.GetString ("Sound") == "enabled")
            {
                soundOn = true;
                float volume = PlayerPrefs.GetInt ("SoundVolume") * 0.01f;
                audioclip.volume = volume;
            }
            else
                soundOn = false;
        }
        DisplayState (0);
    }


    void FixedUpdate ()
    {
        BallCheck ();
    }

    void OnCollisionEnter2D (Collision2D collInfo)
    {
        checkBall = false;
        
		if (collInfo.collider.tag == "Player")
        {
            GetComponent <Rigidbody2D> ().velocity = new Vector2 (
                GetComponent <Rigidbody2D> ().velocity.x,
                GetComponent <Rigidbody2D> ().velocity.y * ballMOD +
                collInfo.gameObject.GetComponent <Rigidbody2D> ().velocity.y * playerMOD);
        }
    }

    void OnCollisionExit2D()
    {
        checkBall = true;
    }

    void BallCheck()
    {
        Vector3 ballSpeed = GetComponent <Rigidbody2D> ().velocity;

        if (stopBall)
        {
            GetComponent <Rigidbody2D> ().velocity = Vector2.zero;
            transform.position = Vector2.zero;
        }

        if (checkBall && !stopBall)
        {
            if (ballSpeed.x <= 15 && ballSpeed.x > 0)
                GetComponent <Rigidbody2D> ().velocity = new Vector2 (18f, ballSpeed.y);
            else if (ballSpeed.x >= -15 && ballSpeed.x < 0)
                GetComponent <Rigidbody2D> ().velocity = new Vector2 (-1 * 18f, ballSpeed.y);
        }
    }

    void DisplayState(int stateID)
    {	
        for (int i = 0; i < states.Length; i++)
        {
            if (i == stateID)
            {
                spriteRenderer.sprite = states[i];
				for (int j = 0; j < 5 ; j++) 
				{
					string text = "m_Colors.m_Color[" + j.ToString() +"]";
					sObject.FindProperty(text).colorValue = colors[i][j];
				}
				sObject.ApplyModifiedProperties();
            }
        }
    }

    void ChangeState(float min, float max)
    {
        float random = Random.Range (min, max);
        Vector3 speed = GetComponent <Rigidbody2D> ().velocity;
        
		PlayAudio (random);
        GetComponent <Rigidbody2D> ().velocity = new Vector2 (speed.x * random, speed.y);
    }
   	
	void CreateColors ()
	{
		colors = new Color32[3][];
		//		{new Color32(0, 255, 150, 255), new Color32(255, 186, 0, 255), new Color32(255, 42, 0, 255)}
		for (int i = 0; i < 3; i++)
		{
			colors[i] = new Color32[5];

			for (int j = 0; j < 5; j++)
			{
				
				if (i == 0)
				{
					byte alpha = (byte) (16 * (16 - j) -1);
					colors[i][j] = new Color32 (0, 255, 150, alpha);
//					string text = "Colors[" + i.ToString() +"][" + j.ToString() + "] = " + colors[i][j].ToString();
//					Debug.Log(text);
				}
				if (i == 1)
				{
					byte alpha = (byte) (16 * (16 - j) -1);
					colors[i][j] = new Color32 (255, 186, 0, alpha);
//					string text = "Colors[" + i.ToString() +"][" + j.ToString() + "] = " + colors[i][j].ToString();
//					Debug.Log(text);
				}
				if (i == 2)
				{
					byte alpha = (byte) (16 * (16 - j) -1);
					colors[i][j] = new Color32 (255, 42, 0, alpha);
//					string text = "Colors[" + i.ToString() +"][" + j.ToString() + "] = " + colors[i][j].ToString();
//					Debug.Log(text);
				}
			}
		}
	}

	void BallHit()
    {
        isHit = true;
        DisplayState (2);
        ChangeState (1.15f, 1.33f);
    }

    void PlayerHit()
    {
        isHit = false;
        DisplayState (0);
        ChangeState (0.67f, 0.85f);
    }

    void NormalHit()
    {
        isHit = false;
        DisplayState (1);
        ChangeState (0.90f, 1.10f);
    }

    IEnumerator ResetBall ()
    {
        checkBall = false;
		trailRenderer.enabled = false;
		GetComponent <Rigidbody2D> ().isKinematic = true;
        GetComponent <Rigidbody2D> ().velocity = Vector2.zero;
        yield return new WaitForSeconds (1);
        DisplayState (-1);
        transform.position = Vector3.zero;
        isHit = false;
        StartCoroutine(GoBall());
    }


    IEnumerator GoBall ()
    {
        DisplayState (0);
        yield return new WaitForSeconds(2);
        if (!stopBall)
        {
            int[] array = new int[2] { -1, 1 };
            int x = Random.Range(0, 2);
            int y = Random.Range(0, 2);
            float dirX = array[x] * Random.Range(0.75f, 1.25f);
            float dirY = array[y] * Random.Range(0.75f, 1.25f);

            GetComponent <Rigidbody2D> ().AddForce (new Vector2 (xForce * dirX, yForce * dirY));
            checkBall = true;
			GetComponent <Rigidbody2D> ().isKinematic = false;
			trailRenderer.enabled = true;
        }
    }

    public void Ready()
    {
        stopBall = false;
        readyButton.SetActive (false);
        StartCoroutine (GoBall ());
    }

    void PlayAudio (float random)
    {
        if (soundOn)
        {
            audioclip.pitch = random;
            audioclip.Play ();
        }
    }
}
