using UnityEngine;
using System.Collections;

public class MainSceneGUI : MonoBehaviour
{
	void OnGUI()
	{
		if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 50), "Bouncing Ball"))
		{
			Application.LoadLevel("VerletScene");
		}
		if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2, 100, 50), "Spring"))
		{
			Application.LoadLevel("SpringScene");
		}
		if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 50, 100, 50), "Exit"))
		{
			Application.Quit();
		}
	}
}
