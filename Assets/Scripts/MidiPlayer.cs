using System;
using UnityEngine;
using CSharpSynth.Sequencer;
using CSharpSynth.Synthesis;
using CSharpSynth.Midi;

[RequireComponent (typeof(AudioListener))]

public class MidiPlayer : MonoBehaviour
{
    public float gain = 1f;
    public int sampleBufferSize = 1024;
    public static readonly string bankPath = "GM Bank/gm";
	public string MidiFilePath = "Midis/canyon.mid";

    // Channel, Note, Velocity
    public event Action<int, int, int> OnNoteOn;

    // Channel, Note
    public event Action<int, int> OnNoteOff;

    public event Action<MidiEvent> OnOtherMidiEvent;

    private StreamSynthesizer synthesizer;
    private MidiSequencer sequencer;
    private float[] sampleBuffer;
    private string midiFilePath;

    void Awake()
    {
        synthesizer = new StreamSynthesizer (44100, 2, sampleBufferSize, 40);
        sampleBuffer = new float[synthesizer.BufferSize];     
        synthesizer.LoadBank(bankPath);
        sequencer = new MidiSequencer (synthesizer);

        //These will be fired by the sequencer when a song plays. Check the console for messages
        sequencer.NoteOnEvent += TriggerNoteOn;
        sequencer.NoteOffEvent += TriggerNoteOff;
        sequencer.OtherMidiEvent += TriggerMidiEvent;
    }

    public void StartMidi(string midiFilePath)
    {
        //sequencer.Stop(true);
        this.midiFilePath = midiFilePath;
        sequencer.LoadMidi(midiFilePath, false);
        sequencer.Play();
    }
	
	public void Play(){
		StartMidi(MidiFilePath);
	}

    private void TriggerNoteOn(int channel, int note, int velocity)
    {
        if (OnNoteOn != null) OnNoteOn(channel, note, velocity);
    }

    private void TriggerNoteOff(int channel, int note)
    {
        if (OnNoteOff != null) OnNoteOff(channel, note);
    }

    private void TriggerMidiEvent(MidiEvent evt)
    {
        if (OnOtherMidiEvent != null) OnOtherMidiEvent(evt);
    }

    void OnAudioFilterRead (float[] data, int channels)
    {
        //This uses the Unity specific float method we added to get the buffer
        if (synthesizer == null ) return;
		synthesizer.GetNext (sampleBuffer);
            
        for (int i = 0; i < data.Length; i++) {
            data[i] = sampleBuffer[i] * gain;
        }
    }
}
