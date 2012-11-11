// david deckman coss

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BGVisuals : MonoBehaviour {
	
    public MidiPlayer midiPlayer;
	public bool[] ActiveChannels = new bool[16];
	
	public Color PeakColour;
	public float Speed;
	private Color normalColour;
	private Color peakSaturation;
	
	private float lerpT = 0f;
	private int lerpDir = 0;
	//private float 

	void Start(){
		normalColour = Camera.main.backgroundColor;
		
		midiPlayer.OnNoteOn += ProcessNoteOn;
	}
	
	void Update(){
		
		//go backwards if at max
		if (lerpT >= 1) lerpDir = -1;
		//stop if at min and going backwards
		else if (lerpT <= 0 && lerpDir == -1) lerpDir = 0;

		//increment/decrement lerpT
		lerpT += lerpDir * Speed * Time.deltaTime;

		//change colour
		Camera.main.backgroundColor = Color.Lerp(normalColour, PeakColour * peakSaturation, lerpT);
		
		if (Input.GetKeyDown(KeyCode.F)){
			lerpDir = 1;
			Debug.Log(lerpDir);
		}
	}
	
	public void ProcessNoteOn(int channel, int note, int velocity){
				
		if (channel < ActiveChannels.Length && !ActiveChannels[channel]) return;
		
		float saturation = velocity / 127.0f;
		peakSaturation = new Color(saturation, saturation, saturation);

		//begin flash
		lerpDir = 1;
	}
	
	
}
