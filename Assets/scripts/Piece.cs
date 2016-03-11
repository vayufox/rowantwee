using UnityEngine;
using System.Collections;

public class Piece : MonoBehaviour {
	public int x;
	public int y;
	public int type;
	
	bool onGrid = false;
	
	private RectTransform rect;
	
	public float Width { get { return rect.rect.width; } }
	public float Height { get { return rect.rect.height; } }
	
	public bool stopped = false;
	
	// Use this for initialization
	void Awake() {
		rect = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void Register(int cellX, int cellY) {
		x = cellX;
		y = cellY;
		onGrid = true;
	}
	
	public void OnMouseUp() {
		Game.instance.HandleClick(this);
	}
	
	public override string ToString()
	{
		return "Piece: " + x + " " + y;
	}
}
