using UnityEngine;
using System;

public class MidionLevel : ScriptableObject
{
    public TextAsset midiFile;
    public ChannelSetting[] timeline;
    public GameObject[] channelPrefabMap = new GameObject[16];

    [Serializable]
    public class ChannelSetting
    {
        public float time;
        public bool[] activeChannels = new bool[16];
        public bool[] BGVisualsChannels = new bool[16];
        public Color PeakColour;
        public Color normalColour;
    }

}
