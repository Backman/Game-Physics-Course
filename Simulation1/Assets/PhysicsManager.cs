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
	public TimeStep TimeStep;
	public float Threshold = 0.00001f;
	public float Gravity = 9.82f;
	public Sphere[] Spheres;

	void Update()
	{
		float numerator = TimeStep.Numerator;
		float denominator = Mathf.Clamp(TimeStep.Denominator, 0.1f, float.MaxValue);
		float dt = numerator / denominator;

		UpdateSpheres(dt);

		CheckCollisions();
	}

	void UpdateSpheres(float dt)
	{
		foreach (Sphere s in Spheres)
		{
			s.ClearForce();

			Vector3 gravityForce = new Vector3(0.0f, -Gravity * s.Mass, 0.0f);

			s.addForce(gravityForce);

			s.PhysicsUpdate(dt);
		}
	}

	void CheckCollisions()
	{
		Vector3 N = new Vector3(0f, 1f, 0f);
		foreach(Sphere s in Spheres)
		{
			float v = Vector3.Dot(N, s.Velocity);

			if (s.transform.position.y <= s.Radius && v < -Threshold)
			{
				Vector3 vel = s.Velocity - N * (1f + s.Dampening) * v;
				s.Velocity = vel;
			}
		}
	}
}
