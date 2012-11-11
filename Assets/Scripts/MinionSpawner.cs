using UnityEngine;
using System.Collections;
using CSharpSynth.Midi;

public class MinionSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public MidiPlayer midiPlayer;
    public int noteOnsBetweenEachSpawn;

    public GameObject[] prefabsToSpawn;

    void Start()
    {
        midiPlayer.OnNoteOn += ProcessNoteOn;
        midiPlayer.OnNoteOff += ProcessNoteOff;
        midiPlayer.OnOtherMidiEvent += ProcessOtherMidiMessage;
        midiPlayer.Play();
    }

    int noteOnCounter = 0;
    private void ProcessNoteOn(int channel, int note, int velocity)
    {
        if (noteOnCounter % noteOnsBetweenEachSpawn == 0) {
            if (prefabsToSpawn.Length >= 1)
                Object.Instantiate(prefabsToSpawn[0], spawnPoint.position, Quaternion.identity);
        }
        noteOnCounter++;
    }

    private void ProcessNoteOff(int channel, int note)
    {
    }

    private void ProcessOtherMidiMessage(MidiEvent midiEvent)
    {
    }
}
