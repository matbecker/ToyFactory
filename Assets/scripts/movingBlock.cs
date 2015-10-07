using UnityEngine;
using System.Collections;

public class movingBlock : MonoBehaviour {

	[SerializeField] Player1 player;
	[SerializeField] Vector3 xExtend;
	[SerializeField] float speed;
	[SerializeField] Vector3 currentPos;
	[SerializeField] Vector3 startPos;
	[SerializeField] Vector3 extendToX;
	[SerializeField] float extendAmount;
	private bool startMoving;
	private Vector2 knockBackDirection;
	[SerializeField] float knockBackPower;
	[SerializeField] Animator anim;
	public bool pushBlock;
	public bool riseBlock;
	public bool moveBlock;
	[SerializeField] int triggerHit;
	[SerializeField] int riseAmount;
	[SerializeField] Vector3 yExtend;
	[SerializeField] Vector3 extendToY;
	[SerializeField] BoxCollider2D extraTrigger;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
		extendToX = new Vector3(extendAmount,0f,0f);
		extendToY = new Vector3(0f,extendAmount,0f);
		yExtend = transform.position - extendToY;
		xExtend = transform.position - extendToX;
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player1>();
		triggerHit = 0;
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player") && moveBlock && transform.position.y < player.transform.position.y)
		{
			startMoving = true;

			anim.SetBool("blockTouched", startMoving);
		}
		if (other.CompareTag("Player") && pushBlock && player.wallCheck)
		{
			startMoving = true;
			anim.SetTrigger("pushBlock");
		}
		if (other.CompareTag("Player") && riseBlock)
		{
			triggerHit++;

			if (triggerHit >= riseAmount)
			{
				//other.transform.parent = currentPos;
				startMoving = true;
				anim.SetTrigger("riseBlock");
			}
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			startMoving = false;
			anim.SetBool("blockTouched", startMoving);
		}
		if (pushBlock)
		{
			startMoving = false;
			extraTrigger.enabled = false;
		}
		//other.transform.parent = null;
	}
	void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("Player") && pushBlock && player.wallCheck)
		{
			startMoving = true;
		}
	}
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.CompareTag("Player") && transform.position.y > player.transform.position.y)
		{
			knockBackDirection = other.collider.transform.position - transform.position;
			player.KnockBack(knockBackPower, knockBackDirection);
		}
		if (other.collider.CompareTag("Player") && pushBlock && player.wallCheck)
		{
			extraTrigger.enabled = true;
		}
	}
	// Update is called once per frame
	void Update () {


		if (startMoving)
		{
			if (!riseBlock)
			{
				transform.position = Vector3.Lerp(transform.position, xExtend, Time.deltaTime * speed);
			}
			if (riseBlock)
			{
				transform.position = Vector3.Lerp (transform.position, yExtend, Time.deltaTime * speed);
			}
		}
		if (!startMoving) 
		{
			currentPos = transform.position;
			transform.position = Vector3.Lerp (currentPos, startPos, Time.deltaTime * speed);
		}
	
	}
}
