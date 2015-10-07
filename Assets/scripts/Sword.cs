using UnityEngine;
using System.Collections;

public class Sword : MonoBehaviour {

	public Player1 player;
	public float damage;
	[SerializeField] SpriteRenderer sprite;
	[SerializeField] Sprite sword;

	public Animator Anim { get; private set; }
	// Use this for initialization
	void Start () {
		//transform.rotation = Quaternion.Euler(0, 0, 0);
		Anim = GetComponent<Animator> ();
		if(player != null)
			transform.parent.gameObject.SetActive (false);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (player == null) {
			if (collider.gameObject.CompareTag ("Player")) {
				//parent sword to player game object
				//transform.parent = collider.transform;
				//collider.gameObject.GetComponent<Player1> ().EquipSword ();
				Destroy(gameObject);
			}
		} else {
			if (player.hasSword) {
				//if the sword collides with something that isnt a trigger and is tagged as an enemy
				//var breakableObject = collider.GetComponent<breakableObject>();
				//if (breakableObject != null) {
				//	(breakableObject as IDamagable).TakeDamage(10f);
				//}
				var damagable = collider.GetComponent<IDamagable>();
				//if the collider colides with something that isnt Idamagable
				if(damagable != null){
					damagable.TakeDamage(10f);
				}
			}
		}
	}
}
