using UnityEngine;
using System.Collections;

public class playerAttack : MonoBehaviour {

	public bool attacking;
	public float attackTimer;
	public float attackCooldown;
	public Collider2D attackTrigger;
	public Player1 player;
	public Sword swordScript;
	// Use this for initialization
	void Start () {
		//disable the sword trigger
		if (player.hasSword) 
		{
			//attackTrigger.enabled = false;
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (player.hasSword) {
			//when the player pushes s and isnt attacking
			if (Input.GetKeyDown ("x") && !attacking) {
				//the player is now attacking
				attacking = true;
				//assign the timer to the value of the cooldown timer
				attackTimer = attackCooldown;
				//turn the sword trigger on
				//attackTrigger.enabled = true;

				swordScript.Anim.SetTrigger ("Attack");
			}
			//when the player attacks
			if (attacking) {

				//and the timer is greater than 0
				if (attackTimer > 0.0f) {
					//intialize the cooldown
					attackTimer -= Time.deltaTime;
				} else {
					//reset the attack bool
					attacking = false;
					//turn off the attack trigger
					//attackTrigger.enabled = false;
				}
			}
		}
	}
}
