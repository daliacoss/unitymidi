using UnityEngine;
using System.Collections;

public class IfEditor : MonoBehaviour
{

	void Start()
    {
        if (Application.isEditor) {
            if (CrossSceneComm.levelToPlay == null) {
                Application.LoadLevel("LevelSelect");
            }
        }
	}
}
