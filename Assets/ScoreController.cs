using UnityEngine;
using System.Collections;

public class ScoreController : Singleton<ScoreController> {


	public int pts;

	// Use this for initialization
	void Start () {
		pts = 0;
	}
	void OnGUI(){
		GUI.Label (new Rect (Screen.width/2 - 50, 0, 100, 20), "Score: " + pts);
	}

	public void addScore(int bonus){
		this.pts += bonus;
	}
	public void Restart(){
		pts = 0;
	}
}
