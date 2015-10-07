using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SwapUIImage : MonoBehaviour {

	[SerializeField] Image image;
	[SerializeField] Sprite swappedImage;
	public ToyPiece toyPiece;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (toyPiece.hasPiece)
		{
			image.sprite = swappedImage;
		}
	
	}
}
