using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponBase : MonoBehaviour {

	//any other class can get the damage but cant set the damage
	[SerializeField] protected float Damage;
	[SerializeField] int MaxReserveAmmo;
	[SerializeField] float ReloadTime;
	[SerializeField] float FireRate;
	[SerializeField] int Ammo;
	[SerializeField] int ReserveAmmo;
	[SerializeField] int ClipSize;
	float fireRateTimer;
	[SerializeField] Sprite WeaponSprite;
	private bool isReloading;

	// Use this for initialization
	public virtual void Start () {
		//display the current weapon possessed in the UI
		hud.instance.WeaponImage.sprite = WeaponSprite;
	}
	
	// Update is called once per frame
	public virtual void Update () {
		//when the timer has been set
		if (fireRateTimer > 0) 
		{		
			//count down the timer
			fireRateTimer -= Time.deltaTime;
		}

		//when the player isnt trying to attack, they have reserve ammo and their ammo is less than their clipSize
		else if (Input.GetKeyDown ("r") && ReserveAmmo > 0 && Ammo < ClipSize) {
			//playef is reloading 
			isReloading = true;
			//call the reload function after reloadTime seconds 
			Invoke("Reload", ReloadTime);
		}
		//display the amount of ammo the player has both in their clip and in their reserve
		hud.instance.AmmoText.text = string.Format ("Ammo: {0} / {1}", Ammo, ReserveAmmo);
	}

	public virtual void UpdateAnimInfo(bool isMoving){}

	public virtual void OnUnequip(){
		//disable the game object
		gameObject.SetActive (false);
	}

	public virtual void OnEquip(){
		//enable the game object
		gameObject.SetActive (true);
		hud.instance.WeaponImage.sprite = WeaponSprite;
		//display the weapon type when the player equips their weapon in the UI
		hud.instance.WeaponImage.gameObject.SetActive (true);
		//display the amount of ammo for the weapon when the player equips their weapon in the UI
		hud.instance.AmmoText.gameObject.SetActive (true);
	}

	//All the checks to see if we can Attack (ex. fire a bullet)
	public void TryAttack(){
		//when the timer is less than or equal to zero, the clipsize doesnt equal -1 or their ammo is greater than zero and they arent reloading
		if (fireRateTimer <= 0 && (ClipSize == -1 || (Ammo > 0 && !isReloading))) {
			//initiate attack function 
			DoAttack(false);
		}
	}

	//Acutally Firing the Bullet
	public virtual void DoAttack(bool skip){
		//set the timer to equal the Fire Rate amount
		fireRateTimer = FireRate;
		//decrease amount of ammo
		Ammo--;
	}

	public virtual void Reload(){
		//amount to reload is the clipsize minus the amount of ammo currently in the clip 
		var reloadAmount = ClipSize - Ammo;
		//if the reserveAmmo minus the reloadAmount is less than 0 
		if (ReserveAmmo - reloadAmount < 0) {
			//reload the remaining amount in the reserveAmmo
			reloadAmount = ReserveAmmo;
		}
		//decrease the reserveAmmo based on how much ammo the player reloaded
		ReserveAmmo -= reloadAmount;
		//increase the ammo based on how much ammo the player reloaded 
		Ammo += reloadAmount;
		//reset the reloading bool because the player just reloaded 
		isReloading = false;
	}

}
