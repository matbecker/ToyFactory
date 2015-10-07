using UnityEngine;
using System.Collections;

public class PickupWeapon : MonoBehaviour {
	[SerializeField] int weaponIndex;

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player")){
			other.GetComponent<Player1>().AddWeapon(weaponIndex, true);
			Destroy(transform.parent.gameObject);
		}

	}
}
