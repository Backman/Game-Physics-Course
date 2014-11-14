using UnityEngine;
using System.Collections;

public class SpringSceneGUI : MonoBehaviour
{
	private Spring _spring;
	private VerletBall _ball;
	public void Start()
	{
		_spring = FindObjectOfType<Spring>();
		_ball = FindObjectOfType<VerletBall>();
	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			_spring.SpringConstant *= 2f;
		}
		if (Input.GetKeyDown(KeyCode.W))
		{
			_spring.SpringConstant /= 2f;
		}

		if (Input.GetKey(KeyCode.Z))
		{
			_spring.DampingCoefficient += 0.05f;
		}
		if (Input.GetKey(KeyCode.X))
		{
			_spring.DampingCoefficient -= 0.05f;
		}

		if (Input.GetKey(KeyCode.E))
		{
			_ball.Mass += 0.1f;
		}
		if (Input.GetKey(KeyCode.R))
		{
			_ball.Mass -= 0.1f;
		}

		if (Input.GetKey(KeyCode.F))
		{
			_spring.RestLength += 0.1f;
		}
		if (Input.GetKey(KeyCode.G))
		{
			_spring.RestLength -= 0.1f;
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

		
		GUI.Label(new Rect(150f, 10f, 300f, 100f), "Spring constant: " + _spring.SpringConstant);
		GUI.Label(new Rect(300f, 10f, 300f, 100f), "Dampening coefficient: " + System.Math.Round(_spring.DampingCoefficient, 3));
		GUI.Label(new Rect(450f, 10f, 300f, 100f), "Rest length: " + System.Math.Round(_spring.RestLength, 2));
		GUI.Label(new Rect(600f, 10f, 300f, 100f), "Ball mass: " + System.Math.Round(_ball.Mass, 2));
		GUI.Label(new Rect(750f, 10f, 300f, 100f), "Ball elasticity: " + System.Math.Round(_ball.Elasticity, 2));

		GUI.Label(new Rect(10f, 50, 500f, 50f), "Press Q and W to regulate spring constant");
		GUI.Label(new Rect(10f, 70f, 500f, 50f), "Press Z and X regulate dampening constant");
		GUI.Label(new Rect(10f, 90, 500f, 50f), "Press F and G regulate rest length");
		GUI.Label(new Rect(10f, 110f, 500f, 50f), "Press E and R regulate ball mass");
		GUI.Label(new Rect(10f, 130f, 500f, 50f), "Press T and Y regulate ball elasticity");
		GUI.Label(new Rect(10f, 150f, 500f, 50f), "Add horizontal force to ball with A/D or Left/Right arrow");
	}
}
