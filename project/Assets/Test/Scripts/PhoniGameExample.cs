using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Phoni;

public class PhoniGameExample : MonoBehaviour {
	
	
	public int localPort = 3000;
	public bool useFixedPort = false;
	
	public PhoniControllerForUnity phoniController;
	
	// Use this for initialization
	void Start () {
		phoniController.CommandEventFromPlayers += CommandHandler;
	}
	
	// Update is called once per frame
	void Update () {		
		
		if(PhoniInput.Player.Count>0) {
			//Debug.Log(PhoniInput.Player[0].StandardData.ReceivedData.gyro.rotationRate);
			//PhoniInput.Player[0].CustomState.SendingData = PhoniInput.Player[0].CustomState.ReceivedData;
			if(PhoniInput.Player[0].TouchData.ReceivedData.TouchCount > 0) {
				Debug.Log(PhoniInput.Player[0].TouchData.ReceivedData.touches[0]);
			}
		}
				
	}
		
	void OnGUI() {
		if(!PhoniGameController.IsStarted) {
			//GUI.Label(new Rect(40,40,100,40), "listen port");
			//localPort = int.Parse(GUI.TextField(new Rect(40,60,200,20),localPort.ToString()));
			if(GUI.Button(new Rect(40,100,80,40), "Start Server")) {
				if(useFixedPort) {
					PhoniGameController.GameClientStart(localPort);
				}
				else {
					PhoniGameController.GameClientStart(0);
				}
				//PhoniInput.Player.CommandEvent += CommandHandler;
			}
		}
		else {
			GUI.Label(new Rect(40,40,100,40), "local IP: "+PhoniGameController.GameIPAddress);
			GUI.Label(new Rect(40,80,100,40), "local port: "+PhoniGameController.GamePort);	
		}
		if(PhoniInput.Player.Count > 0 && PhoniInput.Player[0].IsActive) {
			GUI.Label(new Rect(40, 180, 100, 20), "Player 0:");
			GUI.Label(new Rect(40, 200, 100, 20), PhoniInput.Player[0].CustomState.ReceivedData);
			
			if(GUI.Button(new Rect(40, 250, 80, 40), "Rumble")) {
				PhoniInput.Player[0].SendCommand(PhoniCommandCode.COMMAND_RUMBLE);
			}
		}
	}
	
	private void CommandHandler(PhoniDataPort port, PhoniCommandInfo info) {
		PhoniDataBase data = info.data;
		switch(info.command) {
		case PhoniCommandCode.COMMAND_TEST:
			//Debug.Log("############# " + ((PhoniCustomDataSample)data).intData);
			//Debug.Log("============ " + ((PhoniCustomDataSample)data).stringData);
			Debug.Log(data.GetData<List<string>>()[0] + data.GetData<List<string>>()[1]);
			break;
		}
	}
	
	void OnApplicationQuit() {
		PhoniGameController.GameClientShutdown();
	}
	
	
	public Texture2D CaptureImage (Camera camera)
    {
		int width = 300;
		int height = 200;
        Texture2D captured = new Texture2D (width, height);
		
		RenderTexture rt = new RenderTexture(width, height, 24);
		camera.targetTexture = rt;
        camera.Render();
        RenderTexture.active = rt;
        captured.ReadPixels(new Rect(0,0,width,height),0,0);
        captured.Apply();
		camera.targetTexture = null;
        RenderTexture.active = null;
		DestroyImmediate(rt);
        return captured;
    }
}
