using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HP : MonoBehaviour {

	public Canvas canvas;
	public GameObject healthPanel;
	private Text[] texts;
	private Slider hpSlider;

	public float offsetX = 0;
	public float offsetY = 1;
	public float offsetZ = 0;
	public int current_hp;
	public int max_hp;
	// Use this for initialization
	void Start () {
		texts = new Text[2];
		healthPanel = Instantiate (Resources.Load ("hp")) as GameObject;
		healthPanel.transform.SetParent (Player.Instance.GetComponent<HP>().canvas.transform, false);
		texts = healthPanel.GetComponentsInChildren<Text> ();
		hpSlider = healthPanel.GetComponentInChildren<Slider> ();
		string name = "";
		if (this.gameObject.name == "Ryushin") {
			max_hp = 100;
			current_hp = max_hp;
			name = "Ryushin";
		} else {
			max_hp = 40;
			current_hp = max_hp;
			name = "Guard";
		}
		texts [0].text = name;
		texts [1].text = max_hp + "/" + max_hp;
	}
	
	// Update is called once per frame
	void Update () {
		hpSlider.value = (float)current_hp / (float)max_hp;
		texts[1].text = current_hp + "/" + max_hp;

		Vector3 worldPos = new Vector3 (transform.position.x + offsetX, transform.position.y + offsetY, transform.position.z + offsetZ);
		Vector3 screenPos = Camera.main.WorldToScreenPoint (worldPos);
		Debug.Log (screenPos);
		healthPanel.transform.position = new Vector3 (screenPos.x, screenPos.y, screenPos.z);
	}

	public bool getDamage(){
		current_hp -= 10;
		if (current_hp == 0) {
			healthPanel.SetActive (false);
		}
		return current_hp > 0;
	}

	public void Restart(){
		current_hp = max_hp;
		healthPanel.SetActive (true);
	}
}
