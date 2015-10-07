using UnityEngine;
using System.Collections;

public class EnemyTank : Enemy {

	[SerializeField] float maxSpeed;
	[SerializeField] GameObject bullet;
	[SerializeField] float bulletSpeed;
	[SerializeField] float bulletDelay;
	[SerializeField] ParticleSystem smokeParticles;
	private bool attackPlayer;
	private float timer;
	private float attackTimer;
	private bool shootCannonball;



	public override void OnTriggerEnter2D(Collider2D other)
	{
		base.OnTriggerEnter2D(other);
		if (other.CompareTag("Player"))
		{
			attackPlayer = true;
			attackTimer = 0f;
		}
		if (other.CompareTag("boundaryBox"))
		{
			FlipVelocity();
			FlipScaleX();
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			attackPlayer = false;
		}
	}
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.CompareTag("Player") && attackPlayer)
		{

			var knockbackDirection = other.collider.transform.position - transform.position;
			//push the player in the opposite direction when chasing them
			player.KnockBack(1000f, knockbackDirection);
		}
		if (other.collider.CompareTag("TempObject"))
		{
			FlipScaleX();
			FlipVelocity();
		}
	}
	// Use this for initialization
	public override void Start () 
	{
		base.Start ();
		attackPlayer = false;
	}
	
	// Update is called once per frame
	public override void Update () 
	{
		base.Update();
		timer += Time.deltaTime;
		if (attackPlayer)
		{
			attackTimer += Time.deltaTime;
			if(attackTimer >= 0.5f)
			{
				smokeParticles.Play ();
				attackTimer = 0f;
				DoAttack();
			}
			FollowPlayer();
		}
		anim.SetBool ("shootPlayer", attackPlayer);
		rb.velocity = new Vector2(xVelocity, yVelocity);

		if (rb.velocity.x > maxSpeed) 
		{
			rb.velocity  = new Vector2(maxSpeed, rb.velocity.y);
		}
		if (rb.velocity.x < -maxSpeed) 
		{
			rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
		}
		if (health <= 0f)
		{
			Die();
		}
		if (xVelocity <= 0)
		{
			smokeParticles.startSpeed = 5;
		}
		if (xVelocity >= 0)
		{
			smokeParticles.startSpeed = -5;
		}
	
	}
	public override void FlipScaleX ()
	{
		base.FlipScaleX ();
	}
	public override void FollowPlayer ()
	{
		base.FollowPlayer ();
		targetDirection.y = 0f;
	}
	public override void Die ()
	{
		base.Die ();
	}
	public override void FlipVelocity ()
	{
		base.FlipVelocity ();
	}
	public override void DoAttack()
	{
		base.DoAttack();
		//direction of the shot is the difference in the position of the player and the gun barrel
		Vector2 shotDirection = player.transform.position - shootPoint.transform.position;
		//shoot stright 
		shotDirection.y = 0;
		//normalize the direction so it fires at a consistent rate
		shotDirection.Normalize();
		//draw a bullet 
		var bulletClone = Instantiate (bullet, shootPoint.transform.position, Quaternion.identity) as GameObject;
		//give the bullet a velocity based on the shotDirection multiplied by the bullet speed
		bulletClone.GetComponent<Rigidbody2D>().velocity = shotDirection * bulletSpeed;
		bulletClone.GetComponent<bullet>().damage = 2;
	}
	
}
