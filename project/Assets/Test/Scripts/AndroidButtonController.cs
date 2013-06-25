using UnityEngine;
using System.Collections.Generic;

public class AndroidButtonController : MonoBehaviour {
	
	public Button[] buttons;
	public float maxDepth = 50;
	
	private Dictionary<GameObject, string> buttonDic = new Dictionary<GameObject, string>();
	public static Dictionary<string, bool> buttonStatus = new Dictionary<string, bool>();

	// Use this for initialization
	void Start () {
		foreach(Button button in buttons) {
			buttonDic[button.buttonObj] = button.name;
			buttonStatus[button.name] = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		List<string> resetList = new List<string>(buttonStatus.Keys);
		foreach(Touch touch in Input.touches) {
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0));
			RaycastHit[] hitInfo = Physics.RaycastAll(ray, maxDepth);
			foreach(RaycastHit hit in hitInfo) {
				string name;
				if(buttonDic.TryGetValue(hit.collider.gameObject, out name)) {
					buttonStatus[name] = true;
					resetList.Remove(name);
				}
			}
		}
		foreach(string name in resetList) {
			buttonStatus[name] = false;
		}
	}
	
	[System.Serializable]
	public class Button {
		public string name;
		public GameObject buttonObj;
	}
}
