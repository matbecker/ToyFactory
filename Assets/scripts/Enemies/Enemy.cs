using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour, IDamagable {

	public float health;
	[SerializeField] float maxRotateSpeed;
	[SerializeField] protected ParticleSystem explosionParticles;
	[SerializeField] protected Rigidbody2D rb;
	[SerializeField] protected Animator anim;
	[SerializeField] protected Player1 player;
	[SerializeField] protected Vector2 OppositeTargetDirection;
	[SerializeField] protected float angleDifference;
	[SerializeField] protected float xVelocity;
	[SerializeField] protected float yVelocity;
	protected Vector2 targetDirection;
	[SerializeField] protected Transform shootPoint;



	public virtual void Start()
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player1>();
	}

	public virtual void Update()
	{
		
	}

	public virtual void TakeDamage (float dmg)
	{
		health -= dmg;
	}

	public virtual void Attack(){}

	public virtual void LookAtPlayer()
	{
		OppositeTargetDirection = transform.position - player.transform.position;
		var targetRotation = helperFunctions.Vector2ToDirection(OppositeTargetDirection);
		
		angleDifference = targetRotation - transform.eulerAngles.z;
		var rotateAmount = Mathf.Clamp(angleDifference, -maxRotateSpeed, maxRotateSpeed);
		
		transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0f, 0f, rotateAmount));
	}

	public virtual void FollowPlayer()
	{
		//direction the tank should head in 
		targetDirection = player.transform.position - transform.position;
		//normalize the direction
		targetDirection.Normalize();
		transform.position += (player.transform.position - transform.position).normalized * 0.05f;
	}
	public virtual void FlipScaleX(){
		var temp = transform.localScale;
		temp.x *= -1;
		transform.localScale = temp;
	}
	public virtual void DoAttack()
	{

	}
	public virtual void TryAttack()
	{
		
	}
	public virtual void FlipVelocity()
	{
		xVelocity *= -1f;
		yVelocity *= -1f;
	}
	public virtual void Die()
	{
		ParticleSystem explosionEffect = Instantiate(explosionParticles) as ParticleSystem;

		explosionEffect.transform.position = transform.position;

		explosionEffect.Play();

		//Destroy(explosionEffect.gameObject, explosionEffect.duration);

		Destroy (gameObject);
	}
	public virtual void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("breakablePillar"))
		{
			return;
		}
	}
	public virtual void ShootAtTarget(GameObject bullet, bool shootStraight)
	{
		targetDirection = player.transform.position - transform.position;
		targetDirection.Normalize();
		if (shootStraight)
		{
			targetDirection.y = 0f;
		}
		//create a new laser when the player is within range
		var Clone = Instantiate (bullet, shootPoint.transform.position, Quaternion.identity) as GameObject;
		//give the laser a velocity thats equal to the difference between the player and turret and multiply it by the lasers speed
		Clone.GetComponent<Rigidbody2D> ().velocity = targetDirection * xVelocity;
	}
	
}
