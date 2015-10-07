using UnityEngine;
using System.Collections;

public class hiddenBlock : MonoBehaviour {
	
	public bool disableCollider;
	public float timer;
	[SerializeField] SpriteRenderer sprite;
	public Animator anim;

	// Use this for initialization
	void Start () 
	{
		anim = gameObject.GetComponent<Animator>();
	}
	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.CompareTag("Player"))
		{
			anim.SetBool("playerBehindBlock", true);
		}
	}
	void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.CompareTag("Player"))
		{
			anim.SetBool("playerBehindBlock", false);
		}
	}

	// Update is called once per frame
	void Update () 
	{
		//if the collider is disabled
		if (disableCollider) 
		{
			//start the timer
			timer += Time.deltaTime;
		}
		//if the player pushes down
		if (Input.GetButtonDown("Duck")) 
		{
			//the collider is not enabled
			disableCollider = true;
			//disable the collider
			gameObject.GetComponent<Collider2D> ().enabled = false;
		} 
		//when the timer reaches 0.6
		if (timer > 0.6f)
		{
			//the collider is enabled 
			disableCollider = false;
			//reenable the collider
			gameObject.GetComponent<Collider2D> ().enabled = true;
			//reset the timer
			timer = 0.0f;
		}
	}
}
