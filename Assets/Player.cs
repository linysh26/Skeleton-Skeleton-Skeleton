using UnityEngine;
using System.Collections;

public class Player : Singleton<Player> {

	public Animator anima;
	public int status;
	public bool isAlive = true;
	public bool duringUnmovableAction = false;
	public AnimatorStateInfo stateInfo;

	public float speed;
	public float walk_speed = 3.0f;
	public float run_speed = 5.0f;

	// Use this for initialization
	void Start () {
		anima = GetComponent<Animator> ();
		this.GetComponent<Rigidbody> ().isKinematic = false;
	}
	
	// Update is called once per frame
	public void Update () {
		if (anima == null)
			return;
		if (!isAlive) {
			anima.Play ("Death");
			stateInfo = anima.GetCurrentAnimatorStateInfo (0);
			return;
		}
		stateInfo = anima.GetCurrentAnimatorStateInfo (0);
		if (stateInfo.IsName ("Idle")) {
			speed = 0;
			duringUnmovableAction = false;
		}
		if (duringUnmovableAction)
			return;
		if (Input.GetMouseButtonDown (0)) {
			SwingAttack ();
			duringUnmovableAction = true;
			return;
		} else if (Input.GetKeyDown (KeyCode.Space) && !stateInfo.IsName("Jump")) {
			JumpAttack ();
		}
		else
			Move ();
	}

	void SwingAttack(){
		anima.Play ("SwingQuick");
		stateInfo = anima.GetCurrentAnimatorStateInfo (0);
	}

	void JumpAttack(){
		anima.Play ("Jump");
		stateInfo = anima.GetCurrentAnimatorStateInfo (0);
	}

	void Move(){
		float translationY = Input.GetAxis ("Horizontal");
		float translationX = Input.GetAxis ("Vertical");
		Vector3 nextPosition = this.transform.position;
		Vector3 forward = Camera.main.transform.forward;
		Vector3 right = Camera.main.transform.right;
		right.y = 0;
		forward.y = 0;
		nextPosition += translationX * forward + translationY * right;
		if (nextPosition != this.transform.position) {
			if (stateInfo.IsName ("Jump")) {
				
			} else if (Input.GetKey (KeyCode.LeftShift)) {
				anima.Play ("Run");
				speed = run_speed * 4;
			} else {
				anima.Play ("Walk01");
				speed = walk_speed * 4;
			}
			this.transform.LookAt (nextPosition);
			gameObject.GetComponent<Rigidbody> ().velocity = gameObject.transform.TransformDirection (Vector3.forward) * speed;
		} else if (!stateInfo.IsName ("Jump")) {
			anima.Play ("Idle");
			gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
		}
		stateInfo = anima.GetCurrentAnimatorStateInfo (0);
		//set the mirror position for the camera to look at and rotate around
		Camera.main.transform.parent.position = this.transform.position;
	}

	void Recurrent(){
		anima.Play ("Resurrection");
		stateInfo = anima.GetCurrentAnimatorStateInfo (0);
		duringUnmovableAction = true;
	}

	public void Restart(){
		isAlive = true;
		this.transform.position = new Vector3 (4.79f, 1, -3.98f);
		Camera.main.transform.parent.position = this.transform.position;
		Recurrent ();
	}
}
