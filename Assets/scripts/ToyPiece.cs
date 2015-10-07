using UnityEngine;
using System.Collections;

public class ToyPiece : MonoBehaviour  {
	
	public bool hasPiece;
	public string name;
	[SerializeField] GameObject planePiece;
	public ToyObject toy;


	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			hasPiece = true;

			if (name == "middle" || name == "back" || name == "front" || name == "wing" || name == "thruster")
			{
				toy.piecesAcquired++;
			}
			planePiece.SetActive(false);


		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{

		}
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		
	}
}
