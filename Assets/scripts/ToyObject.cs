using UnityEngine;
using System.Collections;

public class ToyObject : MonoBehaviour {

	public int piecesAcquired;
	public int totalToyPieces = 5;
	public bool hasAllPieces;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (piecesAcquired == totalToyPieces)
		{
			hasAllPieces = true;
		}
	
	}
}
