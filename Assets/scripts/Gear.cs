using UnityEngine;
using System.Collections;

public class Gear : MonoBehaviour {

	[SerializeField] float gearSpeed;
	[SerializeField] Player1 player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player1>();
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			player.Damage(1f);
		}
	}
	// Update is called once per frame
	void Update () {
		transform.Rotate(0f,0f,gearSpeed * Time.deltaTime);
	}
}
