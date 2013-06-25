using UnityEngine;
using System.Collections;
using Phoni;

public class ShowTouch : MonoBehaviour {
	
	public int playerNum;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		if(PhoniInput.Player.Count>playerNum) {
			if(PhoniInput.Player[playerNum].TouchData.IsValid &&
				PhoniInput.Player[playerNum].TouchData.ReceivedData.TouchCount > 0) {
				renderer.enabled = true;
				Vector2 pos = PhoniInput.Player[playerNum].TouchData.ReceivedData.touches[0].position;
				transform.position = Camera.main.ScreenToWorldPoint(new Vector3(pos.x,pos.y,17));			
			}
			else {
				renderer.enabled = false;
			}
		}
		
				
	}
}
