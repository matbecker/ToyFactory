using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {

	[SerializeField] float speed;
	[SerializeField] Transform top;
	[SerializeField] Transform bottom;
	//[SerializeField] Animator anim;
	private Vector3 startPos;
	private float yVelocity;
	private bool goingUp;
	private bool goingDown;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			other.transform.parent = transform;
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			other.transform.parent = null;
		}
	}
	// Use this for initialization
	void Start () {
		startPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {


		if(transform.position.y >= top.position.y && goingUp && !goingDown)
		{
			yVelocity = 0;
			SetElevatorDown();
		}
		if(transform.position.y <= bottom.position.y && goingDown && !goingUp)
		{
			yVelocity = 0;
		}
		transform.Translate(Vector3.up * yVelocity * Time.deltaTime);

	}
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.CompareTag("Player"))
		{
			SetElevatorUp();
		}
	}
	void OnCollisionExit2D(Collision2D other)
	{
		if (other.collider.CompareTag("Player"))
		{
			SetElevatorDown();
		}
	}
	void SetElevatorDown()
	{
		CancelInvoke("ElevatorUp");
		Invoke("ElevatorDown", 1f);
		goingDown = false;
		goingUp = false;
	}

	void SetElevatorUp()
	{
		CancelInvoke("ElevatorDown");
		Invoke ("ElevatorUp", 1f);
		goingUp = false;
		goingDown = false;
	}

	void ElevatorUp()
	{
		yVelocity = speed;
		goingUp = true;
	}
	void ElevatorDown()
	{
		yVelocity = -speed;
		goingDown = true;
	}
}
