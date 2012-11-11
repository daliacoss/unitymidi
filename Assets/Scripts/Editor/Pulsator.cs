using UnityEngine;

public class Pulsator : MonoBehaviour
{
    private int beatCount = 0;
    private float lastBeatTime = float.MinValue;


    void Update()
    {
        float secondsPerBeat = 1f / MidiPlayer.instance.BPM / 60f;
        if (Time.time - lastBeatTime > secondsPerBeat) {
            ;
        }
    }
}
