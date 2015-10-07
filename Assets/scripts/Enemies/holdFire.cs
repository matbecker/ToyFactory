using UnityEngine;
using System.Collections;

public class holdFire : MonoBehaviour {

	[SerializeField] Turret turret;

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.CompareTag("Player"))
		{
			turret.canShoot = false;
			turret.laserTimer = 0f;
		}
	}
	void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.CompareTag("Player"))
		{
			turret.canShoot = true;
		}
	}
}
