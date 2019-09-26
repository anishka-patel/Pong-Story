using UnityEngine;
using System.Collections;

public class GameSetup : MonoBehaviour 
{

	public Camera mainCam;

	public GameObject topWall;
	public GameObject bottomWall;
	public GameObject leftWall;
	public GameObject rightWall;

	public GameObject player01;
	public GameObject player02;

	private float playerOffset = 125.0f ;

	void Awake () 
    {
        if (PlayerPrefs.HasKey ("DefaultSettings"))
            playerOffset = PlayerPrefs.GetInt ("PlayerOffset") * 1.0f;
		
		topWall.transform.position = new Vector2 ( 
		                                          	0f , 
		                                          	mainCam.ScreenToWorldPoint ( new Vector3 (0f , Screen.height, 0f) - new Vector3 (0f, 1f, 0f)).y);
		bottomWall.transform.position = new Vector2 ( 
		                                            0f , 
		                                            mainCam.ScreenToWorldPoint ( new Vector3 (0f, 0f, 0f) + new Vector3(0f, 1f, 0f)).y);
		leftWall.transform.position = new Vector2 ( 
		                                           	mainCam.ScreenToWorldPoint ( new Vector3 (10f, 0f, 0f)).x , 
		                                           	mainCam.ScreenToWorldPoint ( new Vector3 (0f, Screen.height *0.5f, 0f)).y);
		rightWall.transform.position = new Vector2 (
													mainCam.ScreenToWorldPoint (new Vector3 (Screen.width - 10f , 0f, 0f)).x, 
													mainCam.ScreenToWorldPoint (new Vector3 (0f, Screen.height * 0.5f, 0f)).y);

		player01.transform.position = new Vector2 (
													mainCam.ScreenToWorldPoint ( new Vector3 (playerOffset, 0f, 0f)).x , 
													mainCam.ScreenToWorldPoint ( new Vector3 (0f,Screen.height * 0.5f,0f)).y);
		player02.transform.position = new Vector2 (
													mainCam.ScreenToWorldPoint ( new Vector3 (Screen.width - playerOffset, 0f, 0f)).x, 
													mainCam.ScreenToWorldPoint ( new Vector3 (0f, Screen.height * 0.5f, 0f)).y);
	}

	void Update () 
    {
		topWall.transform.position = new Vector2 ( 
		                                       		0f , 
		                                       		mainCam.ScreenToWorldPoint ( new Vector3 (0f , Screen.height, 0f) - new Vector3 (0f, 1f, 0f)).y);
		bottomWall.transform.position = new Vector2 ( 
			                                        0f , 
			                                        mainCam.ScreenToWorldPoint ( new Vector3 (0f, 0f, 0f) + new Vector3(0f, 1f, 0f)).y);
		leftWall.transform.position = new Vector2 ( 
			                                       	mainCam.ScreenToWorldPoint ( new Vector3 (10f, 0f, 0f)).x , 
			                                       	mainCam.ScreenToWorldPoint ( new Vector3 (0f, Screen.height *0.5f, 0f)).y);
		rightWall.transform.position = new Vector2 (
													mainCam.ScreenToWorldPoint (new Vector3 (Screen.width - 10f , 0f, 0f)).x, 
													mainCam.ScreenToWorldPoint (new Vector3 (0f, Screen.height * 0.5f, 0f)).y);
		
		player01.transform.position = new Vector2 (
													mainCam.ScreenToWorldPoint ( new Vector3 (playerOffset, 0f, 0f)).x , 
													player01.transform.position.y);
		player02.transform.position = new Vector2 (
													mainCam.ScreenToWorldPoint ( new Vector3 (Screen.width - playerOffset, 0f, 0f)).x, 
													player02.transform.position.y);
	}

	public void ResetPosition () 
    {
		topWall.transform.position = new Vector2 ( 
		                                          	0f , 
		                                          	mainCam.ScreenToWorldPoint ( new Vector3 (0f , Screen.height, 0f) - new Vector3 (0f, 1f, 0f)).y);
		bottomWall.transform.position = new Vector2 ( 
		                                            0f , 
		                                            mainCam.ScreenToWorldPoint ( new Vector3 (0f, 0f, 0f) + new Vector3(0f, 1f, 0f)).y);
		leftWall.transform.position = new Vector2 ( 
		                                           	mainCam.ScreenToWorldPoint ( new Vector3 (10f, 0f, 0f)).x , 
		                                           	mainCam.ScreenToWorldPoint ( new Vector3 (0f, Screen.height *0.5f, 0f)).y);
		rightWall.transform.position = new Vector2 (
													mainCam.ScreenToWorldPoint (new Vector3 (Screen.width - 10f , 0f, 0f)).x, 
													mainCam.ScreenToWorldPoint (new Vector3 (0f, Screen.height * 0.5f, 0f)).y);

		player01.transform.position = new Vector2 (
													mainCam.ScreenToWorldPoint ( new Vector3 (playerOffset, 0f, 0f)).x , 
													player01.transform.position.y);
		player02.transform.position = new Vector2 (
													mainCam.ScreenToWorldPoint ( new Vector3 (Screen.width - playerOffset, 0f, 0f)).x, 
													player02.transform.position.y);

	}
}
