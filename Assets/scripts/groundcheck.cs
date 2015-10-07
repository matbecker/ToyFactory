using UnityEngine;
using System.Collections;

public class groundcheck : MonoBehaviour {

	private Player1 player;
	[SerializeField] Transform Groundcheck;
	[SerializeField] Transform Groundcheck2;
	[SerializeField] LayerMask collisionLayer;

	void Start() 
	{
		player = gameObject.GetComponentInParent<Player1> ();
		player.canJump = false;
	}
	void Update()
	{
		player.canJump = Physics2D.Linecast(Groundcheck.position, Groundcheck2.position, collisionLayer, -1f,1f);

		if(Input.GetKeyDown ("a")){
			Debug.Log(Groundcheck.position + " " + Groundcheck2.position);
		}
	}	
}
