using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid {
	private int cols;
	private int rows;
	
	private float width;
	private float height;
	
	private float offsetX;
	private float offsetY;
	
	Piece[,] cells;
	
	int[,] scratch;
	
	private RectTransform parent_;
	public Grid(int numCols, int numRows, RectTransform parent) {
		cols = numCols;
		rows = numRows;
		parent_ = parent;
		cells = new Piece[cols, rows];
		scratch = new int[cols, rows];
		width = parent_.rect.width;
		height = parent_.rect.height;
	}
	
	public void UpdatePosition(Transform obj, float w, float h, int cellX, int cellY) {
		obj.localPosition = new Vector3(w/2 + ((width / cols) * cellX), h/2 +((height / rows) * cellY), 0);
		
	}
	
	public void UpdatePiecePosition(Piece piece) {
		piece.transform.localPosition = new Vector3(piece.Width/2 + ((width / cols) * piece.x), piece.Height/2 +((height / rows) * piece.y), 0);
	}
	
	public void Place(Piece piece, int cellX, int cellY) {
		cells[cellX,cellY] = piece;
		piece.Register(cellX,cellY);
		piece.transform.SetParent(parent_, false);
		UpdatePiecePosition(piece);	
	}
	public bool IsValid(Piece piece, int cellX, int cellY) {
		return piece != null && cells[cellX, cellY] == piece;
	}
	
	public void Swap(int aX, int aY, int bX, int bY) {
		Piece a = cells[aX, aY];
		Piece b = cells[bX, bY];
		cells[bX, bY] = a;		
		a.Register(bX, bY);
		UpdatePiecePosition(a);
		cells[aX, aY] = b;		
		b.Register(aX, aY);
		UpdatePiecePosition(b);
	}
	
	public void MoveDown() {
		for(int i = 0; i < cols; i++) {
			for(int j = 0; j < rows; j++) {
				Down(i, j);
			}
		}
	}
	
	public void Remove(int x, int y) {
		Piece p = cells[x, y];
		if(p != null) {
			GameObject.Destroy(p.gameObject);
			cells[x,y] = null;
		}
		
	}
	
	public void ClearMatches() {
		int last = 0;
		int cnt = 0;
		for(int i = 0; i < rows; i++) {
			if(cnt > 1) {
				Debug.Log("Removing type "+ last +":"+cnt );
				for(int x = cols - 1; x > cols - 2 - cnt; x--) {
					Remove(x, i-1);
				}
			}
			last  = 0;
			cnt = 0;
			for(int j = 0; j < cols; j++) {
				int cur = scratch[j,i];
				if(cur == last) {
					if(cur > 0)
						cnt++;
				}
				else {
					if(cnt > 1) {
						Debug.Log("Removing type "+ last +":"+cnt );
						for(int x = j - 1; x > j - 2 - cnt; x--) {
							Remove(x, i);
						}
					}
					cnt = 0;
					last = cur;
				}
			}
		}
	}
	
	public bool Down(int x, int y) {
		Piece p = cells[x, y];
		scratch[x,y] = 0;
		
		if(p == null) {
			return false;
		}
		if(y > 0) {
			if(cells[x, y - 1] == null) {
				cells[x, y] = null;
				cells[x, y - 1] = p;
				p.Register(x, y-1);
				UpdatePiecePosition(p);
				p.stopped = false;
				
				return true;
			}
		}
		p.stopped = true;
		scratch[x,y] = p.type;
		
		return false;
	}
	
	
	
	public bool isFull(int x, int y) {
		return cells[x,y] != null;
	}
	
	
	
}
