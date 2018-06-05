using UnityEngine;
using System.Collections;

public class TestTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collider){
		Player p = Player.Instance;
		if (collider.name[0] == 'g') {
			if (p.anima.GetCurrentAnimatorStateInfo (0).IsName ("SwingQuick")) {
				GuardGeneral.Instance.currentGuard.anima.Play ("Damage");
				ScoreController.Instance.addScore (5);
				collider.GetComponent<Guard>().isAlive = collider.GetComponent<HP> ().getDamage ();
			}
		}
	}
}
