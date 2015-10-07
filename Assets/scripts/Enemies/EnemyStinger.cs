using UnityEngine;
using System.Collections;

public class EnemyStinger : Enemy {

	public bool initializeAttack;
	public bool chasePlayer;
	public bool playerSpotted;
	public bool resetPosition;
	public bool facingRight;
	public bool ignorePlayer;
	public bool inTrigger;
	private Quaternion startRotation;
	[SerializeField] Vector2 attackForce;
	[SerializeField] float chaseSpeed;
	[SerializeField] float timer;
	[SerializeField] float chaseTime;
	// Use this for initialization
	public override void Start () 
	{
		startRotation = transform.rotation;
		base.Start();
		explosionParticles = gameObject.GetComponentInChildren<ParticleSystem>();
		if (transform.rotation.y != 0)
		{
			facingRight = true;
		}
	}
	
	// Update is called once per frame
	public override void Update () 
	{
		base.Update();

		if (chasePlayer)
		{
			timer += Time.deltaTime;
		}
		if (chasePlayer && timer < chaseTime)
		{
			DoAttack();
		}
		if (playerSpotted && timer < chaseTime)
		{
			LookAtPlayer();
		}
		if (resetPosition)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation,startRotation,0.05f);
		}
		if (initializeAttack)
		{
			chasePlayer = true;
			anim.SetBool("attackPlayer", initializeAttack);
			
		}
		if (inTrigger)
		{
			CheckPlayer();
		}
	
	}
	public override void OnTriggerEnter2D(Collider2D other)
	{
		base.OnTriggerEnter2D(other);
		if (other.CompareTag("Player"))
		{
			inTrigger = true;
			player = other.GetComponent<Player1>();
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (!initializeAttack && other.CompareTag("Player"))
		{
			inTrigger = false;
			Reset ();
		}

	}
	void CheckPlayer()
	{
		if (player.transform.position.x < transform.position.x && !facingRight && 
		    player.transform.position.y < transform.position.y || 
		    player.transform.position.x > transform.position.x && facingRight && 
		    player.transform.position.y < transform.position.y)
		{
			playerSpotted = true;
			anim.SetBool("playerSpotted", playerSpotted);
		}

	}
	void Reset()
	{
		playerSpotted = false;
		resetPosition = true;
		anim.SetBool("playerSpotted", playerSpotted);
	}
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.CompareTag("Player"))
		{
			player.Damage(2f);
		}
		if (other.collider.CompareTag("Enemy") || other.gameObject.layer == 13)
		{
			return;
		}
		Die ();
	}
	public override void LookAtPlayer()
	{
		base.LookAtPlayer();
	}
	public override void DoAttack ()
	{
		base.DoAttack ();
		Vector2 targetDir = player.transform.position - transform.position;
		targetDir.Normalize();
		rb.velocity = targetDir * chaseSpeed;
	}
	public override void Die ()
	{
		base.Die ();
	}
}
