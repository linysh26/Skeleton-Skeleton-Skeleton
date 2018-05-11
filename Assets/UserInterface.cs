﻿using UnityEngine;
using System.Collections;


public class UserInterface : MonoBehaviour {

	public Transform character;

	public float speedX;
	public float speedY;

	// Use this for initialization
	void Start () {
		character = this.transform.parent;
		speedY = 50.0f;
		speedX = 10.0f;
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update(){
		//global key behaviours
		if (Input.GetKeyDown(KeyCode.BackQuote)) {
			Cursor.visible = !Cursor.visible;
			if(Cursor.lockState == CursorLockMode.None)
				Cursor.lockState = CursorLockMode.Locked;
			else
				Cursor.lockState = CursorLockMode.None;
		}
		//global mouse behaviours
		if (!Cursor.visible) {
			float translationX = Input.GetAxis ("Mouse X") * speedX;
			float translationY = Input.GetAxis ("Mouse ScrollWheel") * speedY;
			translationX *= Time.deltaTime;
			translationY *= Time.deltaTime;
			this.transform.LookAt (this.transform.parent.position);
			this.transform.RotateAround (this.transform.parent.position, Vector3.right, -translationY * speedY);
			this.transform.RotateAround (this.transform.parent.position, Vector3.up, translationX * speedX);
		}
	}

	void OnGUI(){
		
	}
}
