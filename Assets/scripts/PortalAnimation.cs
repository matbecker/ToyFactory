using UnityEngine;
using System.Collections;

public class PortalAnimation : MonoBehaviour {

	[SerializeField] Player1 player;
	public GameObject otherPortal;

	public void TeleportPlayer()
	{
		if(otherPortal != null){
			player.transform.position = otherPortal.transform.position;
		}
	}

	public void OutOfPortal()
	{
		player.rb.gravityScale = 1f;
	}
	public void TurnGravityOff()
	{
		player.rb.gravityScale = 0f;
		player.rb.velocity = Vector2.zero;
	}
}
