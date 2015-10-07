using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Animation))]

public class AnimationManager : MonoBehaviour {


	float timeAtLastFrame = 0f;
	float timeAtCurrentFrame = 0f;
	float myDeltaTime = 0f;
	AnimationState currentState;
	public Animation anim;
	bool isPlaying = false;
	float startTime = 0f;
	float accumulatedTime = 0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timeAtCurrentFrame = Time.realtimeSinceStartup;
		myDeltaTime = timeAtCurrentFrame - timeAtLastFrame;
		timeAtLastFrame = timeAtCurrentFrame;
		if (isPlaying)
		{
			UpdateAnimation();
		}
	
	}
	public void UpdateAnimation()
	{
		accumulatedTime += myDeltaTime;

		currentState.normalizedTime = accumulatedTime / currentState.length;

		if (accumulatedTime == currentState.length)
		{

			currentState.enabled = false;
			isPlaying = false;
			OnAnimationCompleted();
		}
	}
	void PlayAnimation(string suffix)
	{
		accumulatedTime = 0f;
		currentState = anim[suffix];
		currentState.normalizedTime = 0f;
		currentState.enabled = true;
		isPlaying = true;

	}
	internal void OnAnimationCompleted()
	{
		isPlaying = true;
	}

}
