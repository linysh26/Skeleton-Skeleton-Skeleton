using UnityEngine;
using System.Collections;

public class GuardAttackTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnTriggerEnter(Collider collider){
		Player p = Player.Instance;
		if (collider.name == "skeleton_animated") {
			Guard g = GuardGeneral.Instance.currentGuard;
			if (g.anima.GetCurrentAnimatorStateInfo(0).IsName("Attack")) {
				p.anima.Play ("Hit");
				p.GetComponent<Rigidbody> ().velocity = new Vector3 (0, 0, 0);
				p.duringUnmovableAction = true;
				p.isAlive = ScoreController.Instance.addScore (-5);
			}
		}
	}
}
