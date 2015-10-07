using UnityEngine;
using System.Collections;

public class Turret : Enemy {
	
	public float shotInterval;
	public float laserTimer;
	[SerializeField] GameObject laser;
	public bool canShoot;
	public bool shoot;
	
	// Use this for initialization
	public override void Start () 
	{
		base.Start();
	}
	
	// Update is called once per frame
	public override void Update () 
	{
		base.Update();

		if(laserTimer <= shotInterval && canShoot)
			laserTimer += Time.deltaTime;
		anim.SetBool("canShoot", canShoot);
	}

	public void TryToShoot()
	{
		if (shoot) {
			if (laserTimer >= shotInterval) 
			{
				ShootAtTarget(laser, true);

				laserTimer = 0f;

			}
		}
	}
	public override void OnTriggerEnter2D(Collider2D other)
	{
		base.OnTriggerEnter2D(other);
		if (other.CompareTag ("Player")) 
		{
			canShoot = true;
		}
	}
	void OnTriggerStay2D(Collider2D collider)
	{
		if (collider.CompareTag ("Player") && canShoot) 
		{
			//shoot at the player 
			TryToShoot();	
		}
	}
	void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.CompareTag ("Player")) 
		{
			canShoot = false;
			laserTimer = 0f;
		}
	}
	public override void ShootAtTarget (GameObject bullet, bool shootStraight)
	{
		base.ShootAtTarget (bullet, shootStraight);
	}
}
