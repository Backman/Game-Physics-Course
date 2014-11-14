using UnityEngine;
using System.Collections;

public class EulerPhysicsManager : MonoBehaviour
{
	public GUIText TimeText;
	public float FPS = 60.0f;
	public float PlaneFriction = 1.0f;
	public float Gravity = 9.82f;
	public EulerBall[] EulerBalls;

	private float _threshold = 0.00001f;
	private float _currentTime = 0f;

	void Update()
	{
		FPS = Mathf.Clamp(FPS, 10.0f, float.MaxValue);
		float dt = 1.0f / FPS;

		_currentTime += dt;
		if(TimeText != null)
		{
			TimeText.text = "Time: " + _currentTime;
		}

		UpdateSpheres(dt);

		CheckCollisions();
	}

	void UpdateSpheres(float dt)
	{
		foreach (EulerBall b in EulerBalls)
		{
			Vector3 gravityForce = new Vector3(0.0f, -Gravity * b.Mass, 0.0f);

			b.addForce(gravityForce);

			b.PhysicsUpdate(dt);

			b.ClearForce();
		}
	}

	void CheckCollisions()
	{
		Vector3 N = new Vector3(0f, 1f, 0f);
		foreach (EulerBall b in EulerBalls)
		{
			float v = Vector3.Dot(N, b.Velocity);

			if (Mathf.Abs(b.transform.position.y - 0.0f) < b.Radius && v < -_threshold)
			{
				Vector3 vel = b.Velocity - N * (1f + b.Dampening) * v;
				vel.x *= PlaneFriction;

				b.Velocity = vel;
			}
		}
	}
}
