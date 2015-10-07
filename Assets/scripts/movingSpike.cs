using UnityEngine;
using System.Collections;

public class movingSpike : MonoBehaviour {

	[SerializeField] Player1 player;
	[SerializeField] float extendAmount;
	[SerializeField] float speed;
	[SerializeField] Vector3 currentPos;
	[SerializeField] Vector3 startPos;
	[SerializeField] Vector3 extendTo;
	private Vector3 yExtend;
	private bool dropSpike;
	// Use this for initialization
	void Start () {
		startPos = transform.position;
		extendTo = new Vector3(0f,extendAmount,0f);
		yExtend = transform.position - extendTo;
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player1>();
	
	}
	
	// Update is called once per frame
	void Update () {
		//currentPos = transform.position;
		if (dropSpike)
		{
			transform.position = Vector3.Lerp(transform.position, yExtend, Time.deltaTime * speed);
		}
		else 
		{
			currentPos = transform.position;
			transform.position = Vector3.Lerp (currentPos, startPos, Time.deltaTime * speed);
		}
	
	}
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.CompareTag("Player"))
		{
			player.Damage(1f);
		}
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			dropSpike = true;
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			dropSpike = false;
		}
	}
}
