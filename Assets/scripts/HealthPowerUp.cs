using UnityEngine;
using System.Collections;

public class HealthPowerUp : MonoBehaviour {

	public Player1 player;
	public bool maxHealth;
	public bool activateMaxHealth;
	public bool heartTouched;
	// Use this for initialization
	void Start () 
	{
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player1>();
	}
	void OnTriggerEnter2D(Collider2D collider)
	{

	}
	void MaxHealth(float t)
	{
		if (t == 0)
		{
			return;
		}

		if (heartTouched)
		{
			Debug.Log("max health!");
			player.currentHealth = player.maxHealth;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
