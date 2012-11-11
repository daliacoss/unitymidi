using UnityEngine;

public class Pulsator : MonoBehaviour
{
    public float scaleTo = 3f;
    public int beatScale = 2;

    private float lastBeatTime = float.MinValue;

    private float secondsPerBeat { get { return 1f / MidiPlayer.instance.BPM * 60f; } }
    private Vector3 defaultScale;

    void Awake()
    {
        defaultScale = transform.localScale;
    }

    void Update()
    {
        
        if (Time.time - lastBeatTime >= (secondsPerBeat * (float)beatScale)) {
            lastBeatTime = Time.time;
            Pulsate();
        }
    }

    private void Pulsate()
    {
        Debug.Log("PULSATE!!! " + (secondsPerBeat * (float)beatScale));
        var scaleUp = new Tween(transform, secondsPerBeat * (float)beatScale, new TweenConfig().scale(defaultScale * scaleTo));
        var scaleDown = new Tween(transform, secondsPerBeat * (float)beatScale, new TweenConfig().scale(defaultScale));
        var scaleChain = new TweenChain().append(scaleUp).append(scaleDown);
        scaleChain.play();
    }
}
