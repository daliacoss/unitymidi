using UnityEngine;
using System.Collections;

public class MinionSpawner : MonoBehaviour
{
    public MidiPlayer midiPlayer;

    void Start()
    {
        midiPlayer.OnNoteOn += ProcessNoteOn;
        midiPlayer.OnNoteOff += ProcessNoteOff;
        midiPlayer.OnOtherMidiEvent += ProcessOtherMidiMessage;
    }

    private void ProcessNoteOn(int channel, int note, int velocity)
    {

    }

    private void ProcessNoteOff(int channel, int note)
    {

    }

    private void ProcessOtherMidiMessage(MidiEvent midiEvent)
    {

    }
}
