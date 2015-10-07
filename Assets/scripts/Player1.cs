using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Player1 : MonoBehaviour {

	public float speed = 10.0f;
	public float maxSpeed = 3.0f;
	public float maxJump = 3.0f;
	public float jumpHeight = 100.0f;
	public float currentHealth;
	public float health = 6.0f;
	public float maxHealth = 12.0f;
	public bool canJump;
	public bool wallSliding;
	public bool doubleJump;
	private bool frontFlipAnim;
	public float timer = 0.0f;
	public float damage = 1.0f;
	public bool isMoving = false;
	public bool isInvincible = false;
	public float damageCooldown;
	public float friction = 2.0f;
	public Rigidbody2D rb;
	public Animator anim;
	public cameraFollow screen;
	public Transform wallCheckObject;
	public bool wallCheck;
	public bool hasSword;
	public LayerMask wallLayerMask;
	public LayerMask ceilingLayerMask;
	private bool ceilingCheck;
	[SerializeField] Transform ceilingCheckObject;
	public bool facingRight;
	public int lives;
	public Text liveText;
	[SerializeField] int coinAmount;
	[SerializeField] int resetCoinAmount = 0;
	public Text coinText;
	[SerializeField] ParticleSystem dustParticles;
	[SerializeField] int currentWeapon;
	[SerializeField] List<WeaponBase> weaponSlots;
	[SerializeField] WeaponBase sword;
	[SerializeField] WeaponBase pistol;
	[SerializeField] WeaponBase machineGun;
	public HealthPowerUp hp;
	[SerializeField] float fallTimer;
	[SerializeField] float maxFallTime;
	private bool hurtPlayerOnImpact;
	[SerializeField] Vector3 startPos;
	public bool sendToOtherPortal;
	public Vector3 flagPolePos;
	private bool touchingPushBlock;
	public PortalAnimation portalAnim;
	public bool userInput;
	public bool maxJumpOverride;
	[SerializeField] GameObject toy;
	[SerializeField] int[] toySlots;
	[SerializeField] int currentToy;
	public bool hasToy;

	// Use this for initialization
	
	void Start () 
	{
		rb = gameObject.GetComponent<Rigidbody2D> ();
		currentHealth = health;
		rb.fixedAngle = true;
		facingRight = true;
		hasSword = false;
		lives = 3;
		coinAmount = resetCoinAmount;
		startPos = transform.position;
		userInput = true;
		if (weaponSlots.Count > 0 && currentWeapon < weaponSlots.Count) 
		{
			weaponSlots [currentWeapon].OnEquip ();
		}

	}
	
	// Update is called once per frame
	void Update () 
	{
		//attach the animator bool "grounded" to the canJump variable on the player
		anim.SetBool ("grounded", canJump);
		//attach the animator float "speed" to the velocity of the player 
		anim.SetFloat ("speed", Mathf.Abs (rb.velocity.x));

		anim.SetBool("facingRight", facingRight);
		if (Input.GetAxis ("Horizontal") < 0f && facingRight) 
		{
			FlipScaleX();
			//player is not facing right
			facingRight = false;
		}
		if (Input.GetAxis ("Horizontal") > 0 && !facingRight) 
		{
			FlipScaleX();
			//player is facing right
			facingRight = true;
		}

		if (Input.GetKeyDown ("c")) {
			NextWeapon();
		}
		//when the player hits x try to initiate an attack
		if (Input.GetKey ("x") && weaponSlots.Count > currentWeapon && weaponSlots[currentWeapon] != null) {
			weaponSlots[currentWeapon].TryAttack ();
		} 

		if (Input.GetButton("Jump"))
		{
			if (!canJump || !doubleJump)
			{
				ceilingCheck = Physics2D.OverlapCircle(ceilingCheckObject.position, 0.15f, ceilingLayerMask);
				if (ceilingCheck)
				{
					rb.gravityScale = 0;
				}
				if (!ceilingCheck || Input.GetButtonDown("Jump"))
				{
					rb.gravityScale = 1;
				}
			}
		}
		//if the player pushes the spacebar jump
		if (Input.GetButtonDown ("Jump") && userInput) 
		{
			//if the player can jump and is not sliding down a wall
			if (canJump && !wallSliding) 
			{

				//add a force in the y direction multiplied by the jump height
				rb.AddForce (Vector2.up * jumpHeight);
				//allow the player to double jump
				doubleJump = true;
			} 
			else 
			{
				if (doubleJump) 
				{

					rb.velocity = new Vector2 (rb.velocity.x, 0);
					//jump again
					rb.AddForce (Vector2.up * jumpHeight);
					//set double jump to false because the player has already double jumped
					doubleJump = false;
					//anim.SetTrigger("doubleJump");

				}
					
			}
		}
		//fall damage
		if (!canJump && !wallCheck && !ceilingCheck && userInput)
		{
			fallTimer += Time.deltaTime;

			if (fallTimer > maxFallTime)
			{
				hurtPlayerOnImpact = true;
				fallTimer = 0f;
			}

		}
		if (canJump || wallCheck || ceilingCheck)
		{
			fallTimer = 0f;
		}
		//kill the player if they fall below the map
		if (transform.position.y < -7.0f) 
		{
			playerDeath();
		}
		//dont let the player get more than the max health
		if (currentHealth > health)
		{
			currentHealth = health;
		}

		//if player health is zero kill the player 
		if (currentHealth <= 0) 
		{
			currentHealth = 0;
			playerDeath();
		}
		//if the player cant jump
		if (!canJump || touchingPushBlock) 
		{
			//set the wall check bool to the wallcheck gameobject in front of the player with a radius of 0.1
			wallCheck = Physics2D.OverlapCircle(wallCheckObject.position, 0.1f, wallLayerMask);

			if (facingRight && Input.GetAxis("Horizontal") > 0.1f || !facingRight && Input.GetAxis("Horizontal") < 0.1f)
			{
				//if the wallcheck circle overlaps a sticky wall
				if (wallCheck)
				{
					wallSlide();
				}
			}
		}
		//if the player isnt attached to the wall or they can jump
		if (!wallCheck || canJump) 
		{
			//they arent sliding down a wall
			wallSliding = false;
		}
		if (Input.GetKeyDown("z"))
		{
			toy.SetActive(false);
			//NextToy();
		}
		//append lives to the ui screen
		liveText.text = ("x " + lives);
		//append coin amount to ui screen
		coinText.text = ("x " + coinAmount);
		
	}

	void FlipScaleX()
	{
		var temp = transform.localScale;
		temp.x *= -1;
		transform.localScale = temp;
	}

	public void AddWeapon(int weaponIndex, bool equip){
		var weapon = sword;
		if(weaponIndex == 1)
			weapon = pistol;
		else if(weaponIndex == 2)
			weapon = machineGun;

		weaponSlots.Add(weapon);

		if(equip){
			EquipWeapon(weaponSlots.Count - 1);
		}
	}

	void NextWeapon()
	{
		if(weaponSlots.Count <= 1)
			return;
		var slotIndex = currentWeapon+1;
		//cycle back to first if we are the end of the weapon array
		if (slotIndex >= weaponSlots.Count) 
		{
			slotIndex = 0;
		}
		EquipWeapon(slotIndex);
	}
//	void NextToy()
//	{
//		var slotIndex = currentToy + 1;
//		if (slotIndex >= toySlots.Length)
//		{
//			slotIndex = 0;
//		}
//		EquipToy(slotIndex);
//	}
//	void EquipToy(int slotIndex)
//	{
//		if (currentToy >= 0)
//		{
//
//		}
//		currentToy = slotIndex;
//	}

	void EquipWeapon(int slotIndex){
		if(currentWeapon >= 0){
			//unequip the current weapon
			weaponSlots [currentWeapon].OnUnequip ();
		}
		//change to next weapon
		currentWeapon = slotIndex;
		//equip next weapon
		weaponSlots [currentWeapon].OnEquip ();
	}
	void MaxHealth()
	{
		currentHealth = maxHealth;
	}
	void wallSlide()
	{
		//make the player slide down the wall
		//player is sliding down the wall
		wallSliding = true;

		if (Input.GetButtonDown ("Jump")) 
		{
			if (facingRight)
			{
				rb.AddForce(new Vector2(0.0f,3.0f) * jumpHeight);
			}
			else 
			{
				rb.AddForce(new Vector2(0.0f,3.0f) * jumpHeight);
			}
		}
	}
	void FixedUpdate()
	{
		//input for left and right keys
		float move = Input.GetAxis ("Horizontal");

		rb.AddForce (new Vector2 (move * speed, 0f));
		if (move == 0 && userInput) 
		{
			rb.velocity = new Vector2 (rb.velocity.x / friction, rb.velocity.y);
		}
		if (rb.velocity.magnitude < 0.01f) {
			rb.velocity = Vector2.zero;
		}
		//rb.velocity = new Vector2 (move * speed, rb.velocity.y);
		if (rb.velocity.x != 0.0f && canJump) 
		{
			dustParticles.Play();
		} 
		else 
		{
			dustParticles.Stop();
		}
		if (rb.velocity.x != 0.0f) 
		{
			isMoving = true;
		} 
		else 
		{
			isMoving = false;
		}
		if (weaponSlots.Count > 0 && currentWeapon < weaponSlots.Count) 
		{
			weaponSlots [currentWeapon].UpdateAnimInfo (isMoving);
		}
		//limit player speed
		if (rb.velocity.y > maxJump) 
		{
			if(!maxJumpOverride){
				rb.velocity = new Vector2(rb.velocity.x, maxJump);
			}
		}else if(rb.velocity.y < 0){
			maxJumpOverride = false;
		}
		if (rb.velocity.x > maxSpeed) 
		{
			rb.velocity  = new Vector2(maxSpeed, rb.velocity.y);
		}
		if (rb.velocity.x < -maxSpeed) 
		{
			rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
		}

	}

	public void Damage(float dmg)
	{

		//if the player has been hit and has no cooldown
		if (!isInvincible) 
		{
			//update player health based on damage received
			currentHealth -= dmg;

			//shake the screen when the player gets hit
			screen.ShakeCamera(0.05f,0.5f);

			//play flash animation
			gameObject.GetComponent<Animation> ().Play ("Red_Flash");

			//player is now invincible
			isInvincible = true;
			//set the reset invincible function to equal the float entered for the amount of delay
			StartCoroutine("ResetInvincible", damageCooldown);
		}

	}
	
//	public void TakeDamage (float dmg)
//	{
//		Damage(dmg);
//	}

	public void Damage(float dmg, Vector2 knockbackDirection, float knockbackForce)
	{
		//only damage the player when they are not invincible
		if (!isInvincible) 
		{
			KnockBack(knockbackForce, knockbackDirection);
			Damage(dmg);
		}
	}

	IEnumerator ResetInvincible(float delay){
		//wait for the amount of desired delay seconds 
		yield return new WaitForSeconds (delay);
		//make the player not invincible again so they can get hurt 
		isInvincible = false;
	}
	void OnCollisionEnter2D(Collision2D collider)
	{
		if (hurtPlayerOnImpact)
		{
			Damage(1f);
		}
		if(collider.collider.CompareTag("Platform") || collider.collider.CompareTag("StickyPlatform"))
		{
			//update player position with moving platform position so they dont slide off
			transform.parent = collider.transform;
		}
		if (collider.collider.CompareTag("pushBlock"))
		{
			touchingPushBlock = true;
		}
	}
	void OnCollisionExit2D(Collision2D collider)
	{
		hurtPlayerOnImpact = false;
		if(collider.collider.CompareTag("Platform") || collider.collider.CompareTag("StickyPlatform"))
		{
			//unparent the player from the platform to reset their position
			transform.parent = null;

		}
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Sword")) 
		{
			hasSword = true;
		}
		if (other.CompareTag("Coin"))
		{
			coinAmount++;
			Destroy(other.gameObject);

			if (coinAmount >= 100) 
			{
				coinAmount = resetCoinAmount;
				lives++;
			}
		}
		if (other.CompareTag("portal"))
		{
			anim.SetBool("inPortal", true);
			portalAnim.otherPortal = other.GetComponent<Portal>().otherPortal;
			userInput = false;
		}

		if (other.CompareTag("Heart"))
		{
			if (hp.maxHealth)
			{
				health = maxHealth;
				currentHealth = maxHealth;
			}
			else 
			{
				currentHealth += 2.0f;
			}
			//destroy the heart
			Destroy (other.gameObject);
		}
		if (other.CompareTag ("Life")) 
		{
			lives++;
			//destroy the life
			Destroy(other.gameObject);
		}
		if (other.CompareTag("CompanionToy"))
		{
			other.transform.parent = transform;
			toy = GameObject.FindGameObjectWithTag("CompanionToy");
			hasToy = true;

		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("portal"))
		{
			anim.SetBool("inPortal", false);
			userInput = true;
		}
	}
	public void KnockBack(float knockPower, Vector2 knockDirection)
	{
		//reset the velocity of the player to prevent varying knockback values
		rb.velocity = Vector2.zero;
		knockDirection.Normalize ();
		//add a force in the negative x direction, with the power of the knockback in the y direction
		rb.AddForce(knockDirection * knockPower);
	}

	void playerDeath()
	{
		lives--;
		health = 6f;
		currentHealth = health;
		transform.position = flagPolePos;

		if (lives <= 0) 
		{
			gameOver();
		}
	}
	void gameOver()
	{
		//restart the level
		Application.LoadLevel(Application.loadedLevel);
	}
}
