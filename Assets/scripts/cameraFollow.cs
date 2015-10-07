using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour {
	private Vector2 velocity;
	public GameObject player;
	public float smoothTimeX;
	public float smoothTimeY;
	public bool bounds;
	public Vector3 minCamPos;
	public Vector3 maxCamPos;
	public float shakeTimer;
	public float shakeAmount;
	// Use this for initialization
	void Start () {
		//set player to the game object player
		player = GameObject.FindGameObjectWithTag ("Player");
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		//when the shake time is greater than 0
		if (shakeTimer >= 0.0f) 
		{
			//move camera to any random position within the unit circle of the player
			Vector2 shakePos = Random.insideUnitCircle * shakeAmount;
			//assign various new random camera positions 
			transform.position = new Vector3(transform.position.x + shakePos.x,transform.position.y + shakePos.y, transform.position.z);
			//decrease the timer to break out of the if 
			shakeTimer -= Time.deltaTime;
		}
	}
	public void ShakeCamera(float shakePower, float shakeDuration)
	{
		//set shake amount to the global variable shake power
		shakeAmount = shakePower;
		//set the shake timer to the global variable shake duration
		shakeTimer = shakeDuration;
	}
	void FixedUpdate() {
		//smoothly transform the x position of the camera
		float posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		//smoothly transform the y position of the camera
		float posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);
		//set the camera position to change based of the position of the player
		transform.position = new Vector3 (posX, posY, transform.position.z);

		if (bounds) {
			transform.position = new Vector3 (Mathf.Clamp(transform.position.x, minCamPos.x, maxCamPos.x), 
			                      			  Mathf.Clamp(transform.position.y, minCamPos.y, maxCamPos.y),
			                                  Mathf.Clamp(transform.position.z, minCamPos.z, maxCamPos.z));
			                      
		}
	}
	//set min cam pos to current pos of the camera
	public void setMinCamPos()
	{
		minCamPos = gameObject.transform.position;
	}
	//set max cam pos to current pos of the camera
	public void setMaxCamPos()
	{
		maxCamPos = gameObject.transform.position;
	}
}
