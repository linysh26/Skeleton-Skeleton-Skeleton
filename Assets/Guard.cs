using UnityEngine;
using System.Collections;

public class Guard : MonoBehaviour {

	public static float patrolSideLength = 10;

	public Animator anima;
	public AnimatorStateInfo stateInfo;
	private float speed_raid = 5.0f;
	private float speed_patrol = 2.0f;

	private Transform target;
	private int patrol;
	private Vector3[] patrolRoutine;
	// Use this for initialization
	void Awake () {
		anima = this.GetComponent<Animator> ();
		patrolRoutine = new Vector3[4];
	}
	
	// Update is called once per frame
	public void Update () {
		stateInfo = anima.GetCurrentAnimatorStateInfo (0);
		if (stateInfo.IsName ("Attack") || stateInfo.IsName("Damage"))
			return;
		else if (target != null) {
			Vector3 targetPos = target.position;
			if (getDistanceFrom (targetPos) <= 1.5) {
				Attack ();
			} else {
				Raid (targetPos);
			}
		}
		else {
			Patrol ();
		}
	}

	public void Patrol(){
		this.transform.LookAt (patrolRoutine[patrol]);
		float step = Mathf.Min (speed_patrol * Time.deltaTime, getDistanceFrom(patrolRoutine[patrol]));
		this.transform.position = Vector3.MoveTowards (this.transform.position, patrolRoutine[patrol], step);
		anima.Play ("Walk");
		stateInfo = anima.GetCurrentAnimatorStateInfo (0);
		if (this.transform.position == patrolRoutine [patrol])
			patrol = ++patrol % 4;
	}

	public void Raid(Vector3 targetPos){
		this.transform.LookAt (targetPos);
		float step = speed_raid * Time.deltaTime;
		targetPos.y = 0;
		this.transform.position = Vector3.MoveTowards (this.transform.position, targetPos, step);
		anima.Play ("Run");
		stateInfo = anima.GetCurrentAnimatorStateInfo (0);
	}

	public void Attack(){
		anima.Play ("Attack");
		stateInfo = anima.GetCurrentAnimatorStateInfo (0);
	}

	public void setPatrolRoutine(float x, float z){
		for (int i = 0; i < 2; i++)
			for (int j = 0; j < 2; j++)
				patrolRoutine [i * 2 + j] = new Vector3 (x + i * patrolSideLength, 0, z + j * patrolSideLength); 
	}

	public void removeTarget(){
		this.target = null;
	}

	public void setTarget(Transform target){
		this.target = target;
	}

	public float getDistanceFrom(Vector3 pos){
		float div_x = pos.x - this.transform.position.x;
		float div_z = pos.z - this.transform.position.z;
		return Mathf.Abs(Mathf.Sqrt(div_x * div_x + div_z * div_z));
	}
}