using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public MidionLevel[] levels;
    public int fontSize = 14;
	
	void OnGUI()
    {
        if (Application.isLoadingLevel) return;
        var oldBtnStyle = GUI.skin.button;
        var newBtnStyle = new GUIStyle(oldBtnStyle);
        newBtnStyle.fontSize = this.fontSize;
        GUI.skin.button = newBtnStyle;

        foreach (var level in levels) {
            if (GUILayout.Button(level.name)) {
                CrossSceneComm.levelToPlay = level;
                Application.LoadLevel("midion");
            }
        }
        GUI.skin.button = oldBtnStyle;
	}
}
