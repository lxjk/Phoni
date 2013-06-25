using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Phoni;

public class PhoniPlayerExample : MonoBehaviour {
	
	
	public string ipAddress = "128.2.238.158";
	public int port = 3000;
	
	
	public PhoniControllerForUnity phoniController;
	
	public static string debugMessage= "";
	private string customStr = "off";
	
	
	// Use this for initialization
	void Start () {
		if(Input.gyro.enabled == false) {
			Input.gyro.enabled = true;
		}
		
		phoniController.CommandEventFromGames += CommandHandler;
	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetMouseButtonDown(1)) {
			PhoniInput.Game[0].SendCommand(PhoniCommandCode.COMMAND_TEST,
				//new SimplePhoniData(true));				
				//new PhoniCustomDataSample(2, "yoooo"));
				new PhoniData<List<string>>(new List<string>(new string[] {"hello ", "world"})));
		}
		if(PhoniInput.Game.Count > 0) {
			//Debug.Log(PhoniInput.Game[0].CustomState.ReceivedData);
			PhoniButton buttons = PhoniButton.None;
			if(Input.GetMouseButton(0)) {
				buttons |= PhoniButton.Left;
			}
			if(Input.GetMouseButton(1)) {
				buttons |= PhoniButton.Right;
			}
			PhoniInput.Game[0].ButtonData.SendingData.buttons = buttons;
		}
		
		
		if(PhoniInput.Game[0] != null) {
			PhoniTouchData touchData = PhoniInput.Game[0].TouchData.SendingData;	
			//touchData.touches = PhoniTouch.ConvertTouchArray(Input.touches);
			touchData.multiTouchEnabled = Input.multiTouchEnabled;
			
			PhoniMotionData motionData = PhoniInput.Game[0].MotionData.SendingData;
			motionData.acceleration = Input.acceleration;
			motionData.gyro = new PhoniGyroscope(Input.gyro);
			
			PhoniInput.Game[0].CustomState.SendingData = customStr;
			
			
		}
		
	}
	
	void FixedUpdate () {
	}
	
	
	void OnGUI() {
		if(PhoniInput.Game.Count == 0) {
			GUI.Label(new Rect(240,40,100,20), "server address");
			ipAddress = GUI.TextField(new Rect(240,60,200,20),ipAddress);
			GUI.Label(new Rect(240,80,100,20), "server port");
			port = int.Parse(GUI.TextField(new Rect(240,100,200,20),port.ToString()));
			if(GUI.Button(new Rect(240,120,80,40), "Connect")) {
				PhoniPlayerController.PlayerClientConnect(ipAddress, port);
			}
		}
		else {
			GUI.Label(new Rect(240,40,100,40), "remote IP: "+ipAddress);
			GUI.Label(new Rect(240,80,100,40), "remote port: "+port);
		}
		
		
		GUI.Label(new Rect(240,200,100,20), debugMessage);
		
		if(GUI.Button(new Rect(300, 200, 100, 40),customStr)){
			if(customStr == "off") {
				customStr = "on";
			}
			else {
				customStr = "off";
			}
		}
	}
	
	private void CommandHandler(PhoniDataPort port, PhoniCommandInfo info) {
		PhoniDataBase data = info.data;
		switch(info.command) {
#if UNITY_ANDROID
		case PhoniCommandCode.COMMAND_RUMBLE:
			Handheld.Vibrate();
			break;
#endif
		}
	}
	
	
	void OnApplicationQuit() {
		PhoniPlayerController.PlayerClientDisconnectAll();
	}
	
	void OnApplicationPause(bool pause) {
		foreach(PhoniDataPort game in PhoniInput.Game) {
			game.SendCommand(PhoniCommandCode.COMMAND_SUSPEND, new PhoniData<bool>(!pause));
		}
	}
}
