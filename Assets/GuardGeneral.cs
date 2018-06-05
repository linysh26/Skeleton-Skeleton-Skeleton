using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * this is a observer with a global perspective, 
 * for whom directing all the guards to do with their own deals and 
 * monitoring the player are available.
**/
public class GuardGeneral : Singleton<GuardGeneral>{

	public Guard[,] guards;
	public Player player;

	public Guard currentGuard;

	//global variables
	public float length = 20;
	public float width = 20;
	public int num;

	// Use this for initialization
	void Start () {
		guards = new Guard[3,3];
		num = 9;
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				guards [i,j] = ((GameObject)Instantiate (Resources.Load("guard"), new Vector3((float)i * length + 5, 0, (float)j * width + 5),Quaternion.identity)).GetComponent<Guard> ();
				guards [i,j].setPatrolRoutine (i * length + 5, j * width + 5);
			}
		}
		player = Player.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		player.Update ();
		Vector3 currentPos = player.transform.position;
		if (isValid (currentPos) && player.isAlive) {
			int x = (int)(currentPos.x / length);
			int z = (int)(currentPos.z / width);
			if (currentGuard != null)
				currentGuard.removeTarget ();
			currentGuard = guards [x, z];
			if (currentGuard.isAlive)
				currentGuard.setTarget (player.transform);
			else
				currentGuard = null;
		}
		else if (currentGuard != null)
			currentGuard.removeTarget ();
		for (int i = 0; i < 3; i++)
			for (int j = 0; j < 3; j++)
				if(guards[i,j] != null)
					guards [i,j].Update ();
		
	}

	void OnGUI(){
		if (!player.isAlive) {
			if(currentGuard != null)
				currentGuard.removeTarget ();
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
			if (GUI.Button (new Rect (Screen.width / 2 - 100, 40, 200, 20), "Would you want to revive?")) {
				Restart ();
				Cursor.visible = false;
				Cursor.lockState = CursorLockMode.Locked;
			}
		}
	}

	public bool isValid(Vector3 pos){
		return pos.x >= 0 && pos.x < 60 && pos.z >= 0 && pos.z < 60;
	}

	public void Restart(){
		currentGuard.removeTarget ();
		Player.instance.Restart ();
		ScoreController.instance.Restart ();
		for (int i = 0; i < 3; i++)
			for (int j = 0; j < 3; j++) {
				guards [i, j].Restart ();
				guards [i, j].gameObject.GetComponent<HP> ().Restart ();
			}
	}
}

