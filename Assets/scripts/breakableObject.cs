using UnityEngine;
using System.Collections;

public class breakableObject : MonoBehaviour, IDamagable {

	public float health;
	private Animator anim;

	void Start () {
		anim = gameObject.GetComponent<Animator> ();
	}

	public void TakeDamage (float dmg)
	{
		health -= dmg;
		anim.SetTrigger ("pilar_hit");
		if (health <= 0f) 
		{
			Destroy(gameObject);
		}
	}
}
