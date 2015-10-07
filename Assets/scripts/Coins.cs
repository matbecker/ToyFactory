using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Coins : MonoBehaviour {

	public Player1 player;
	[SerializeField] int coinAmount;
	[SerializeField] int resetCoinAmount = 0;
	[SerializeField] bool coinTouched;
	public Text coinText;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			coinAmount++;
			coinTouched = true;
		}
		if (coinAmount >= 10) 
		{
			coinAmount = resetCoinAmount;
			player.lives++;
		}
	}
	// Use this for initialization
	void Start () {
		coinAmount = resetCoinAmount;
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player1>();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (coinTouched) 
		{
			Destroy(gameObject);
		}
		coinText.text = ("x " + coinAmount);
	
	}
}
