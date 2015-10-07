using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour {
	//player damage
	[SerializeField] float damage = 1.0f;
	//amount of spike knockback 
	[SerializeField] float knockBackForce = 100f;
	//make the player equal nothing
	Player1 player = null;
	Vector2 knockbackDirection;
	void OnTriggerEnter2D (Collider2D other) 
	{
		//if the collider collides with the player 
		if(other.CompareTag("Player"))
		{
			//make the player equal the player 
			player = other.gameObject.GetComponent<Player1>();
			//send the player in the opposite direction they collide with
			knockbackDirection = other.GetComponent<Collider2D>().transform.position - transform.position;

		}
	}
	void OnTriggerExit2D(Collider2D other){
		//reset the player to equal nothing when they leave the spike
		player = null;
	}
	// Update is called once per frame
	void Update () {
		//when the player doesnt equal null
		if (player != null) {
			//perform damage function
			player.Damage(damage, knockbackDirection, knockBackForce);
		}
	}
}
