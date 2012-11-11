using System;
using UnityEngine;
using System.Collections;
using CSharpSynth.Midi;
using Object=UnityEngine.Object;

public class MinionSpawner : MonoBehaviour
{
    public Transform spawnPoint;
	public float radius = 5f;
	
    public MidiPlayer midiPlayer;
    public int noteOnsBetweenEachSpawn;

    public GameObject[] prefabsToSpawn;
	public bool[] activeChannels = new bool[16];

    public Vector3 SpawnPosition { get { return spawnPoint != null ? spawnPoint.position : Vector3.zero; } }

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
		
		var spawnPosition = new Vector3(SpawnPosition.x, UnityEngine.Random.Range(-radius, radius), SpawnPosition.z);

		//if channel is not active/present in activeChannels, don't process
        if (channel < activeChannels.Length && !activeChannels[channel]) return;
		
        if (noteOnsBetweenEachSpawn != 0 && noteOnCounter % noteOnsBetweenEachSpawn == 0) {
            if (prefabsToSpawn.Length >= 1)
                Object.Instantiate(prefabsToSpawn[0], spawnPosition, Quaternion.identity);
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
