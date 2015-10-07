using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class pauseMenu : MonoBehaviour {

	public GameObject PauseOverlay;
	public bool paused = false;
	[SerializeField] Sprite[] sprites;
	[SerializeField] Image image;
	private float lastTime;
	private int currentSprite = 0;
	public GameObject toyPieceUI;

	void Start() 
	{
		//hide the pause screen when the game is started
		PauseOverlay.SetActive (false);
		toyPieceUI.SetActive(false);
	}
	void Update() 
	{
		if (Input.GetButtonDown ("Pause")) 
		{
			//change the pause state
			paused = !paused;
			lastTime = Time.realtimeSinceStartup;
		}
		if (paused) 
		{
			//when the user pushes pause display the pause screen
			PauseOverlay.SetActive(true);
			//stop anything happening in game
			Time.timeScale = 0;
			AnimateSprites();
			toyPieceUI.SetActive(true);
		}
		if (!paused) 
		{
			//when the user pushes pause again hide the pause screen
			PauseOverlay.SetActive(false);
			//continue gameplay
			Time.timeScale = 1;
			toyPieceUI.SetActive(false);
		}

	}
	public void AnimateSprites()
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
	public void Resume() 
	{
		//resume the game when the user clicks resume
		paused = false;
	}
	public void Restart() 
	{
		//reload the current level the user is playing
		Application.LoadLevel(Application.loadedLevel);

	}
	public void MainMenu()
	{
		//load the index of zero which will eventually be a main menu
		Application.LoadLevel (0);
	}
	public void Quit() 
	{
		//quit the game
		Application.Quit ();
	}
}
