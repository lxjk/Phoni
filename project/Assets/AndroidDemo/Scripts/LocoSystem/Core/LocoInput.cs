
//#define TOKYO

using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System;
using LocoCore;


public static class LocoInput {
	
	static public LocoTouch[] Touches{
		get {
			return LocoInputAnalyzer.touchInput.GetTouches();
		}
	}
	
	static public LocoTouch GetTouchFromFingerId(int fingerId) {
		LocoTouch touch = LocoTouch.GetInvalidTouch();
		foreach(LocoTouch data in Touches) {
			if(data.fingerId == fingerId) {
				touch = data;
				break;
			}
		}
		return touch;
	}
	
	static public void RegisterInput(GameObject go) {
		LocoInputAnalyzer.touchInput.receiverSet.Add(go);
	}
	
	static public void UnregisterInput(GameObject go) {
		LocoInputAnalyzer.touchInput.receiverSet.Remove(go);
	}
}


public class LocoTouch {
	public int fingerId;
	public Vector2 position;
	public Vector2 deltaPosition;
	public float deltaTime;
	public int tapCount;
	public TouchPhase phase;
	public bool valid;
	
	public bool IsOnScreen {
		get {
			return valid && phase != TouchPhase.Ended && phase != TouchPhase.Canceled;
		}
	}
	
	public LocoTouch() {
		fingerId = 0;
		position = Vector2.zero;
		deltaPosition = Vector2.zero;
		deltaTime = 0;
		tapCount = 0;
		phase = TouchPhase.Canceled;
		valid = true;
	}
	
	public LocoTouch(Touch touch) {
		fingerId = touch.fingerId;
		position = touch.position;
		deltaPosition = touch.deltaPosition;
		deltaTime = touch.deltaTime;
		tapCount = touch.tapCount;
		phase = touch.phase;
		valid = true;
	}
	
	public override string ToString ()
	{
		StringBuilder sb = new StringBuilder();
		sb.Append("LocoTouch: ");
		sb.Append("fingerId: ");
		sb.Append(fingerId);
		sb.Append(", position: ");
		sb.Append(position);
		sb.Append(", deltaPosition: ");
		sb.Append(deltaPosition);
		sb.Append(", deltaTime: ");
		sb.Append(deltaTime);
		sb.Append(", tapCount: ");
		sb.Append(tapCount);
		sb.Append(", phase: ");
		sb.Append(phase);
		sb.Append(", valid: ");
		sb.Append(valid);
		return sb.ToString();
	}
	
	static public LocoTouch GetInvalidTouch() {
		LocoTouch touch = new LocoTouch();
		touch.valid = false;
		return touch;
	}
}
