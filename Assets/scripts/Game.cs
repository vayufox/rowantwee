using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
	public Piece[] pieces;
	private Grid grid;
	
	public int gridWidth;
	public int gridHeight;
	
	public RectTransform gridParent;
	
	int cx = -1, cy = -1;
	Piece current;
	
	public GameObject outline;
	
	void Awake() {
		instance = this;
	}
	
	// Use this for initialization
	void Start () {
		grid = new Grid(gridWidth, gridHeight, gridParent);
		Populate();	
		outline.SetActive(false);
	}
	

	
	void Populate() {
		for(int j = 0; j < gridHeight; j++) {
			int cnt = 0;
			int last = 0;
			for(int i = 0; i < gridWidth; i++) {
				int pId = Random.RandomRange(0, pieces.Length);
				if(pId == last)
					cnt++;
				else
					cnt = 0;
				if(cnt > 1) {
					pId = (pId + Random.RandomRange(1, pieces.Length)) % pieces.Length;
					cnt = 0;	
				}
				Piece p = Instantiate<GameObject>(pieces[pId].gameObject).GetComponent<Piece>();
				grid.Place(p, i, j);	
				last = pId;
			}
		}
		
	}
	
	public void HandleClick(Piece piece) {
		if(current == null) {
			Debug.Log("Selecting" + piece);
			cx = piece.x;
			cy = piece.y;
			current = piece;
			outline.SetActive(true);
			outline.transform.position = current.transform.position;
		}
		else if(current == piece) {
			Debug.Log("Deselecting" + piece);
			current = null;
			outline.SetActive(false);
		} 
		else {
			if(Mathf.Abs(current.x - piece.x) + Mathf.Abs(current.y - piece.y) == 1) {
				Debug.Log("Swapping " + current + " with " + piece );
				grid.Swap(piece.x, piece.y, current.x, current.y);
				lastTime = Time.time;
			}
				
			
			current = null;
			outline.SetActive(false);
			
		}
	}
	
	public float delay = .5f;
	private float lastTime;
	// Update is called once per frame
	void Update () {
		if(!grid.IsValid(current, cx, cy)) {
			outline.SetActive(false);
			
			current = null;
		}
 		
		if(Time.time - lastTime > delay) {
			lastTime = Time.time;
			grid.MoveDown();
			
			
			grid.ClearMatches();
		}	
	}
	
	
	public static Game instance;
}
