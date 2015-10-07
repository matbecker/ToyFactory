using UnityEngine;
using System.Collections;

public class fallingSpike : MonoBehaviour {

	[SerializeField] Rigidbody2D rb;
	[SerializeField] float respawnTime = 2f;
	private Player1 player;
	private Vector3 startPos;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player1>();
		startPos = transform.position;
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			rb.gravityScale = 2.0f;
			//run ResetPosition after respawnTime
			Invoke("ResetPosition", respawnTime);
		}
	}
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag ("Player")) 
		{
			player.Damage(1.0f);
			gameObject.GetComponent<Collider2D>().enabled = false;
		}

	}
	// Update is called once per frame
	void Update () {
	
	}

	void ResetPosition()
	{
		//reset the position 
		transform.position = startPos;
		//reset the gravity
		rb.gravityScale = 0.0f;
		//reset the velocity
		rb.velocity = Vector2.zero;
		//turn on the trigger
		gameObject.GetComponent<Collider2D>().enabled = true;
		//reset the rotation
		transform.localRotation = Quaternion.identity;

	}
}
