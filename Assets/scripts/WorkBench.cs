using UnityEngine;
using System.Collections;

public class WorkBench : MonoBehaviour {

	public ToyObject toyObject;
	private bool atBench;
	[SerializeField] bool toyAssembled;
	[SerializeField] ParticleSystem ps;
	[SerializeField] GameObject toy;
	[SerializeField] Transform spawnPoint;
	public Player1 player;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			atBench = true;
		}
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () 
	{
	
			if (Input.GetKeyDown("b") && atBench && !player.hasToy && toyObject.hasAllPieces)
			{
				ps.Play();
				toyAssembled = true;
				Instantiate(toy, spawnPoint.position, Quaternion.identity);
				Debug.Log ("Hooray!!!!!!");
			}
		}
	}
