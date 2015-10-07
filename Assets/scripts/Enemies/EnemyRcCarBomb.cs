using UnityEngine;
using System.Collections;

public class EnemyRcCarBomb : Enemy {

	[SerializeField] GameObject boundBoxLeft;
	[SerializeField] GameObject boundBoxRight;
	[SerializeField] BoxCollider2D trigger;
	private bool blowUp;
	public override void Start ()
	{
		base.Start ();
	}
	public override void Update()
	{
		base.Update();

		rb.velocity = new Vector2(xVelocity, yVelocity);

		if (blowUp)
		{
			Destroy(gameObject, 3f);
		}
	}
	public override void FlipScaleX ()
	{
		base.FlipScaleX ();
	}
	public override void OnTriggerEnter2D(Collider2D other)
	{
		base.OnTriggerEnter2D(other);

		if (other.CompareTag("Player"))
		{
			trigger.enabled = false;
			xVelocity *= 3.0f;
			blowUp = true;
			anim.speed = 2.0f;

		}
		if (other.CompareTag("boundaryBox"))
		{
			FlipScaleX();
			FlipVelocity();
		}
	}
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.CompareTag("Player"))
		{
			player.Damage(1f);
			Die ();
		}
		if (other.collider.CompareTag("killBox") && blowUp)
		{
			Die ();
		}
		if (other.collider.CompareTag("Enemy"))
		{
			FlipScaleX();
			FlipVelocity();
		}
	}
	public override void FlipVelocity ()
	{
		base.FlipVelocity ();
	}
	public override void Die ()
	{
		base.Die ();
	}

}
