using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class hud : MonoBehaviour {
	public static hud instance;

	public Sprite[] playerHearts;
	public Image HeartUI;
	public Image WeaponImage;
	public Text AmmoText;
	public Player1 player;

	void Awake(){
		if (instance == null) {
			instance = this;
		} else {
			Debug.LogError("More than one HUD");
		}
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//update UI based on players current health
		HeartUI.sprite = playerHearts [(int)player.currentHealth];

		if (player.currentHealth < 0.0f) 
		{
			player.currentHealth = 0.0f;
		}
	
	}
}
