using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimateSprites : MonoBehaviour {

	[SerializeField] Sprite[] sprites;
	[SerializeField] Image image;
	[SerializeField] Button button;
	[SerializeField] Sprite highlightedSprite;
	public pauseMenu pM;
	private float lastTime;
	private int currentSprite = 0;
	private bool hover = false;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
		if(hover)
		{
			AnimateSprite();
		}


	}
	public void AnimateSprite()
	{
		var currentTime = Time.realtimeSinceStartup;
		if (currentTime - lastTime >= 0.1f)
		{
			lastTime = currentTime;
			currentSprite++;
			
			if (currentSprite >= sprites.Length)
			{
				currentSprite = 0;
			}
		}
		image.sprite = sprites[currentSprite];
		
		
	}
	public void OnHoverEnter()
	{
		lastTime = Time.realtimeSinceStartup;
		//button.spriteState.highlightedSprite = highlightedSprite;
		hover = true;
	}


	public void OnHoverExit(){
		hover = false;
		image.sprite = sprites[0];
	}

}
