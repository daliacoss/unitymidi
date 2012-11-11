using System;
using System.Collections.Generic;
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
	public TextAsset midiFile;

    // Channel, Note, Velocity
    public event Action<int, int, int> OnNoteOn;

    // Channel, Note
    public event Action<int, int> OnNoteOff;

    public event Action<MidiEvent> OnOtherMidiEvent;

    private StreamSynthesizer synthesizer;
    private MidiSequencer sequencer;
    private float[] sampleBuffer;

    private class MidiEventData
    {
        public MidiHelper.MidiChannelEvent eventType;
        public int channel;
        public int note;
        public int velocity;
        public MidiEvent midiEvent;
    }

    private Queue<MidiEventData> eventQueue = new Queue<MidiEventData>();

    void Awake()
    {
        synthesizer = new StreamSynthesizer (44100, 2, sampleBufferSize, 40);
        sampleBuffer = new float[synthesizer.BufferSize];     
        synthesizer.LoadBank(bankPath);
        sequencer = new MidiSequencer (synthesizer);

        //These will be fired by the sequencer when a song plays. Check the console for messages
        sequencer.NoteOnEvent += HandleNoteOn;
        sequencer.NoteOffEvent += HandleNoteOff;
        sequencer.OtherMidiEvent += HandleMidiEvent;
    }

    void Update()
    {
        while (eventQueue.Count > 0) {
            var evt = eventQueue.Dequeue();
            switch(evt.eventType) {
            case MidiHelper.MidiChannelEvent.Note_On:
                TriggerNoteOn(evt.channel, evt.note, evt.velocity);
                break;
            case MidiHelper.MidiChannelEvent.Note_Off:
                TriggerNoteOff(evt.channel, evt.note);
                break;
            case MidiHelper.MidiChannelEvent.Unknown:
                TriggerMidiEvent(evt.midiEvent);
                break;
            }
        }
    }

    public void StartMidi(TextAsset midiFile)
    {
        sequencer.Stop(true);
        sequencer.LoadMidi(midiFile.bytes, false);
        sequencer.Play();
    }
	
	public void Play(TextAsset midiFile=null){
        if (midiFile == null) {
            midiFile = this.midiFile;
        }
		StartMidi(midiFile);
	}

    private void HandleNoteOn(int channel, int note, int velocity)
    {
        eventQueue.Enqueue(new MidiEventData() {
            eventType = MidiHelper.MidiChannelEvent.Note_On,
            channel = channel,
            note = note,
            velocity = velocity
        });
    }

    private void TriggerNoteOn(int channel, int note, int velocity)
    {
        if (OnNoteOn != null) OnNoteOn(channel, note, velocity);
    }

    private void HandleNoteOff(int channel, int note)
    {
        eventQueue.Enqueue(new MidiEventData() {
            eventType = MidiHelper.MidiChannelEvent.Note_Off,
            channel = channel,
            note = note,
        });
    }

    private void TriggerNoteOff(int channel, int note)
    {
        if (OnNoteOff != null) OnNoteOff(channel, note);
    }

    private void HandleMidiEvent(MidiEvent midiEvent)
    {
        eventQueue.Enqueue(new MidiEventData() {
            eventType = MidiHelper.MidiChannelEvent.Unknown,
            midiEvent = midiEvent
        });
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

public static class MidiPlayerPauser
{
    public static MidiPlayer instance;

    public void PauseMidiPlayer()
    {
        if (instance != null) {
;
        }
    }
}
