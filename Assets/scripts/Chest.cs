using UnityEngine;
using System.Collections;

public class Chest : MonoBehaviour {

	[SerializeField] Sprite chestOpen;
	[SerializeField] SpriteRenderer sprite;
	[SerializeField] GameObject weapon;
	[SerializeField] ParticleSystem burst;
	//[SerializeField] ParticleAnimator random;
	[SerializeField] Particle randomcolor;

	bool isOpen = false;

	void OnTriggerEnter2D(Collider2D other)
	{
		//when the player collides with the chest trigger
		if (!isOpen && other.gameObject.CompareTag ("Player")) 
		{
			//change the sprite to the open chest sprite
			sprite.sprite = chestOpen;
			burst.gameObject.SetActive(true);
			//burst.startColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
			/*Color[] randomColors = random.colorAnimation;
			
			for (int i = 0; i < randomColors.Length; i++)
			{
				randomColors[i].r = Random.Range(0.0f, 1.0f);
				randomColors[i].g = Random.Range(0.0f, 1.0f);
				randomColors[i].b = Random.Range(0.0f, 1.0f);
				randomColors[i].a = Random.Range(0.0f, 1.0f);
			}
			random.colorAnimation = randomColors;*/
			//sword.GetComponent<Animation> ().Play ("sword_uncaptured");
			Instantiate(weapon, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);

			isOpen = true;
		}
	}
	// Use this for initialization
	void Start () 
	{
		burst.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{

	
	}
}
