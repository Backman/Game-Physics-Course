using UnityEngine;
using System.Collections;

[System.Serializable]
public class TimeStep
{
	public float Numerator = 1.0f;
	public float Denominator = 60.0f;
}

public class PhysicsManager : MonoBehaviour
{
	public GUIText TimeText;
	public TimeStep TimeStep;
	public float PlaneFriction = 1.0f;
	public float Gravity = 9.82f;
	public Sphere[] Spheres;

	private float _threshold = 0.00001f;
	private float _currentTime = 0f;

	void Update()
	{
		float numerator = TimeStep.Numerator;
		float denominator = Mathf.Clamp(TimeStep.Denominator, 0.1f, float.MaxValue);
		float dt = numerator / denominator;

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
		foreach (Sphere s in Spheres)
		{
			Vector3 gravityForce = new Vector3(0.0f, -Gravity * s.Mass, 0.0f);

			s.addForce(gravityForce);

			s.PhysicsUpdate(dt);

			s.ClearForce();
		}
	}

	void CheckCollisions()
	{
		Vector3 N = new Vector3(0f, 1f, 0f);
		foreach(Sphere s in Spheres)
		{
			float v = Vector3.Dot(N, s.Velocity);

			if (Mathf.Abs(s.transform.position.y - 0.0f) < s.Radius && v < -_threshold)
			{
				Vector3 vel = s.Velocity - N * (1f + s.Dampening) * v;
				vel.x *= PlaneFriction;

				s.Velocity = vel;
			}
		}
	}
}
