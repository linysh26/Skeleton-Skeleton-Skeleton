using UnityEngine;
using System.Collections;

public class ScoreController : Singleton<ScoreController> {


	public int pts;
	public float secondsToHell;

	// Use this for initialization
	void Start () {
		pts = 10;
		secondsToHell = Time.time + 30.0f;
	}

	void Update(){
		if (Time.time >= secondsToHell) {
			pts -= 1;
			secondsToHell += 10.0f;
		}
	}

	void OnGUI(){
		if(pts >= 0)	
			GUI.Label (new Rect (Screen.width / 2 - 20, 40, 40, 20), "pts: " + pts);
	}

	public bool addScore(int bonus){
		this.pts += bonus;
		return pts > 0;
	}
	public void Restart(){
		pts = 10;
		secondsToHell = Time.time + 30.0f;
	}
}
