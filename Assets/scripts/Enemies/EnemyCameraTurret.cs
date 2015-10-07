using UnityEngine;
using System.Collections; 

public class EnemyCameraTurret : WeaponGun {

	[SerializeField] Transform player;
	[SerializeField] Animator anim;
	[SerializeField] float maxRotateSpeed;
	private bool lookAtPlayer;
	private bool resetPosition;
	private bool inTrigger;
	private float angleDifference;
	private float minClamp = 0f;
	private float maxClamp = 179.9f;
	private Vector2 targetDir;
	private Quaternion startRotation;
	public override void Start ()
	{
		base.Start ();
		startRotation = transform.rotation;

	}
	// Update is called once per frame
	public override void Update () {

		base.Update();
		if (inTrigger)
		{
			CheckPlayer();
		}
		if (lookAtPlayer)
		{
			if (player.transform.position.y > transform.position.y)
			{
				Reset();
			}
			else 
			{
				LookAtPlayer();
				if (Mathf.Abs (angleDifference) < 2f)
				{
					TryAttack();
				}
			}
		}
		else if (resetPosition)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation,startRotation,0.05f);
		}
	}
	void OnTriggerEnter2D(Collider2D other)
	{

		if (other.CompareTag("Player"))
		{
			inTrigger = true;
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			inTrigger = false;
			Reset();
		}
	}

	void CheckPlayer(){
		if (player.transform.position.y <= transform.position.y)
		{
			lookAtPlayer = true;
			anim.SetBool("lookAtPlayer", true);
		}
	}

	void Reset(){
		lookAtPlayer = false;
		anim.SetBool("lookAtPlayer", false);
		resetPosition = true;
	}
	void LookAtPlayer()
	{
		targetDir = transform.position - player.transform.position;
		var targetRotation = Mathf.Clamp(helperFunctions.Vector2ToDirection(targetDir), minClamp, maxClamp);

		angleDifference = targetRotation - transform.eulerAngles.z;
		var rotateAmount = Mathf.Clamp(angleDifference, -maxRotateSpeed, maxRotateSpeed);

		transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0f, 0f, rotateAmount));
	}
	public override void DoAttack(bool skip)
	{
		base.DoAttack(true);
		//direction of the shot is the difference in the position of the player and the gun barrel
		Vector2 shotDirection = player.transform.position - shootPoint.transform.position;
		//normalize the direction so it fires at a consistent rate
		shotDirection.Normalize();
		//draw a bullet 
		var bulletClone = Instantiate (bullet, shootPoint.transform.position, 
		    Quaternion.Euler(0f, 0f, helperFunctions.Vector2ToDirection(shotDirection))) as GameObject;
		//give the bullet a velocity based on the shotDirection multiplied by the bullet speed
		bulletClone.GetComponent<Rigidbody2D>().velocity = shotDirection * bulletSpeed;
		bulletClone.GetComponent<bullet>().damage = 1;
	}
}
