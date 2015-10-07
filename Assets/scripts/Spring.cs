using UnityEngine;
using System.Collections;

public class Spring : MonoBehaviour {

	[SerializeField] Animator anim;
	[SerializeField] float jumpHeight;
	[SerializeField] float normalJumpHeight;
	[SerializeField] float maxJumpHeight;
	public bool wasTouchingSpring;
	[SerializeField] float timer;
	[SerializeField] float resetAnimAmount;
	public groundcheck standingOnSpring;
	bool isBouncing;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButton("Jump"))
		{
			jumpHeight = maxJumpHeight;
		}
		else 
		{
			jumpHeight = normalJumpHeight;
		}
	
	}
	void OnCollisionEnter2D(Collision2D other)
	{
		if (!isBouncing && other.collider.CompareTag("Player") && other.transform.position.y > transform.position.y)
		{
			CancelInvoke("ResetSpring");
			other.gameObject.GetComponent<Player1>().maxJumpOverride = true;
			var rb = other.gameObject.GetComponent<Rigidbody2D>();
			rb.velocity = new Vector2(rb.velocity.x, 0f);
			rb.AddForce(Vector2.up * jumpHeight);
			anim.SetBool("onSpring", true);
			isBouncing = true;
		}
	}
	void OnCollisionExit2D(Collision2D other)
	{
		Invoke ("ResetSpring", 1);
		isBouncing = false;
	}

	void ResetSpring(){
		anim.SetBool("onSpring", false);
	}
}
