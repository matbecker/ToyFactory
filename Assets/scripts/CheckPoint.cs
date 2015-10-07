using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

	[SerializeField] Player1 player;
	[SerializeField] Animator anim;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			player.flagPolePos = transform.position;
			anim.SetBool("flagTouched", true);
		}
	}
}
