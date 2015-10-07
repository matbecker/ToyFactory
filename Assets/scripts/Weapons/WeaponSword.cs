using UnityEngine;
using System.Collections;

public class WeaponSword : WeaponBase {

	[SerializeField] Animator Anim;

	public override void UpdateAnimInfo (bool isMoving)
	{
		base.UpdateAnimInfo (isMoving);
		if(Anim != null)
		{
			//play sword sway animation when the player ismoving
			Anim.SetBool ("IsMoving", isMoving);
		}
	}

	public override void DoAttack (bool skip)
	{
		base.DoAttack (false);
		//play attack animation when the player attacks 
		Anim.SetTrigger ("Attack");
	}

	public override void OnEquip ()
	{
		base.OnEquip ();
		hud.instance.AmmoText.gameObject.SetActive (false);
		if(Anim != null)
		{
			//play equipped animation when the player has the sword
			Anim.SetBool ("Equipped", true);
		}
	}

	public override void OnUnequip ()
	{
		base.OnUnequip ();
		if(Anim != null)
		{
			//when the player doesnt have the sword stop the animation
			Anim.SetBool ("Equipped", false);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.isTrigger){
			return;
		}
		var damagable = other.GetComponent<IDamagable>();
		if (damagable != null)
		{
			//damage enemies on collision
			damagable.TakeDamage(Damage);
		}
	}
}
