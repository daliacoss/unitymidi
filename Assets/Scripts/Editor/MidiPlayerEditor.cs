using UnityEngine;
using UnityEditor;

//[CustomInspector(typeof(MidiPlayer))]
public class MidiPlayerEditor : Editor
{
    void Awake()
    {
        //EditorApplication.playmodeStateChanged += 
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
    }

    void HandlePlayStateChange()
    {
        
    }
}
