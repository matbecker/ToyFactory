using UnityEngine;
using System.Collections;

public class InitializeAttack : MonoBehaviour {
	public EnemyStinger stinger;
	//public Turret turret;

	public void SetStingerAttack(){
		stinger.initializeAttack = true;
	}
//	public void SetTurretAttack(){
//		turret.shoot = true;
//	}

}
