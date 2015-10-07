using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {
	public float damage;
	[SerializeField] float destroyTime;
	public Player1 player;
	// Use this for initialization
	void Start () 
	{
		//destroy the bullet after the destroyTime seconds
		Destroy(gameObject, destroyTime);
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player1>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		//dont allow the trigger to collide with another trigger
		if(other.isTrigger || other.gameObject.layer == LayerMask.NameToLayer("invisibleGround")){
			return;
		}
		var damagable = other.GetComponent<IDamagable>();
		if (damagable != null)
		{
			//damage enemies on collision
			damagable.TakeDamage(damage);
		}
		if (other.CompareTag("Player"))
		{
			player.Damage(damage);
		}

		//destroy the bullet on collision
		Destroy(gameObject);
	}
}
