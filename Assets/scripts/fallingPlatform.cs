using UnityEngine;
using System.Collections;

public class fallingPlatform : MonoBehaviour {
	
	[SerializeField] BoxCollider2D col;
	public bool onPlatform;
	public bool ColliderOn;
	[SerializeField] Animator anim;
	[SerializeField] Player1 player;
	[SerializeField] bool horizontalPlatform;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player1>();
	}
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.CompareTag("Player") && player.transform.position.y > transform.position.y || !horizontalPlatform)
		{
			onPlatform = true;
			ColliderOn = true;
			CancelInvoke("ReAppear");
			Invoke("Dissapear", 1f);

		}
	}
	void OnCollisionExit2D(Collision2D other)
	{
		if (other.collider.CompareTag("Player"))
		{
			ColliderOn = true;
			onPlatform = false;
			CancelInvoke("Dissapear");
			Invoke ("ReAppear", 1f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetBool("colliderOn", ColliderOn);
		anim.SetBool("onPlatform", onPlatform);

		if (!col.enabled)
		{
			Invoke("ReAppear", 4f);
		}
	}
	void Dissapear()
	{
		ColliderOn = false;
		col.enabled = false;
		onPlatform = false;
	}
	void ReAppear()
	{
		ColliderOn = true;
		col.enabled = true;
	}
}
