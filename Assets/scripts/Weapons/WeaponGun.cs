using UnityEngine;
using System.Collections;

public class WeaponGun : WeaponBase {
	[SerializeField] protected GameObject bullet;
	[SerializeField] protected Transform shootPoint;
	[SerializeField] protected float bulletSpeed;
	
	// Update is called once per frame
	public override void Update () {
		base.Update ();
	}

	public override void DoAttack (bool skip)
	{
		base.DoAttack (false);
		//break out of the function if skip is set to true
		if (skip) return;
		//direction of the shot is the difference in the position of the player and the gun barrel
		Vector2 shotDirection = shootPoint.transform.position - transform.position;
		//shoot stright 
		shotDirection.y = 0;
		//normalize the direction so it fires at a consistent rate
		shotDirection.Normalize();

		//draw a bullet 
		var bulletClone = Instantiate (bullet, shootPoint.transform.position, Quaternion.identity) as GameObject;
		//give the bullet a velocity based on the shotDirection multiplied by the bullet speed
		bulletClone.GetComponent<Rigidbody2D>().velocity = shotDirection * bulletSpeed;
		//set the bullet damage to equal the Damage specific to the weapon  
		bulletClone.GetComponent<bullet>().damage = Damage;

	}
}
