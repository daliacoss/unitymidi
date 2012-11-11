using UnityEngine;
using UnityEditor;

public static class MidiPlayerPauser
{
    private static bool inited = false;
    static MidiPlayerPauser()
    {
        if (!inited) {
            EditorApplication.playmodeStateChanged += PlayModeStateHandler;
            inited = true;
        }
    }

    public static void PlayModeStateHandler()
    {
        Debug.LogWarning("Statechange!");
        if (MidiPlayer.instance != null) {
            if (EditorApplication.isPaused) {
                MidiPlayer.instance.PauseSequence();
            } else if (EditorApplication.isPlaying) {
                MidiPlayer.instance.UnpauseSequence();
            }
        }
    }
}
