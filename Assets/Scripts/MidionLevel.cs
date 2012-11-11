using UnityEngine;
using System;

public class MidionLevel : ScriptableObject
{
    public TextAsset midiFile;
    public ChannelSetting[] activeChannelTimeline;

    [Serializable]
    public class ChannelSetting
    {
        public float time;
        public bool[] activeChannels = new bool[16];
    }
}
