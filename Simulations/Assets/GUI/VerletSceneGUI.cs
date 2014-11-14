using UnityEngine;
using System.Collections;

public class VerletSceneGUI : MonoBehaviour
{
	private VerletBall _ball;
	public void Start()
	{
		_ball = FindObjectOfType<VerletBall>();
	}

	public void Update()
	{
		if (Input.GetKey(KeyCode.E))
		{
			_ball.Mass += 0.1f;
		}
		if (Input.GetKey(KeyCode.R))
		{
			_ball.Mass -= 0.1f;
		}

		if (Input.GetKeyDown(KeyCode.T))
		{
			_ball.Elasticity += 0.1f;
		}
		if (Input.GetKeyDown(KeyCode.Y))
		{
			_ball.Elasticity -= 0.1f;
		}
	}

	void OnGUI()
	{
		if (GUI.Button(new Rect(0f, 0f, 100f, 50f), "Main Scene"))
		{
			Application.LoadLevel("MainScene");
		}

		GUI.Label(new Rect(150f, 10f, 300f, 100f), "Ball mass: " + System.Math.Round(_ball.Mass, 2));
		GUI.Label(new Rect(300f, 10f, 300f, 100f), "Ball elasticity: " + System.Math.Round(_ball.Elasticity, 2));

		GUI.Label(new Rect(10f, 50f, 500f, 50f), "Press E and R regulate ball mass");

		GUI.Label(new Rect(10f, 70f, 500f, 50f), "Press T and Y regulate ball elasticity");

		GUI.Label(new Rect(10f, 90f, 500f, 50f), "Add horizontal force to ball with A/D or Left/Right arrow");
	}
}
