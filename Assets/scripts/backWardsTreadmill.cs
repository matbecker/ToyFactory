using UnityEngine;
using System.Collections;

public class backWardsTreadmill : MonoBehaviour {


	[SerializeField] Animator anim;
	[SerializeField] float speed;
	// Use this for initialization
	void Start () {
		anim.SetFloat("Speed", speed);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
