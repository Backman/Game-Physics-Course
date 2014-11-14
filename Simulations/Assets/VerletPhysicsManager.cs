using UnityEngine;
using System.Collections;

public class VerletPhysicsManager : Singleton<VerletPhysicsManager> 
{
	public float Gravity = 9.82f;
	public float Timestep = 60f;
	private VerletBall[] Balls;
	private Spring[] Springs;
	private Plane[] Planes;

	public void Start()
	{
		Planes = FindObjectsOfType<Plane>();
		Springs = FindObjectsOfType<Spring>();
		Balls = FindObjectsOfType<VerletBall>();
	}

	public void Update()
	{
		ClearForces();

		Timestep = Mathf.Clamp(Timestep, 10f, 1000f);

		ProcessInput();

		HandleCollision();

		UpdateSprings();
		UpdateVerletBalls();
	}

	private void UpdateSprings()
	{
		foreach (Spring s in Springs)
		{
			UpdateSpring(s);
		}
	}

	private void UpdateVerletBalls()
	{
		foreach (VerletBall b in Balls)
		{
			UpdateVerletBall(b);
		}
	}

	private void HandleCollision()
	{
		foreach (Plane p in Planes)
		{
			foreach (VerletBall b in Balls)
			{
				p.HandleCollision(b);
			}
		}

		foreach (VerletBall b1 in Balls)
		{
			foreach (VerletBall b2 in Balls)
			{
				if (b2 != b1)
				{
					float dist = (b2.Position - b1.Position).magnitude;
					float radii = (b1.Radius + b2.Radius);
					if (dist <= radii)
					{
						float nx = (b2.Position.x - b1.Position.x) / dist;
						float ny = (b2.Position.y - b1.Position.y) / dist;
						float p = 2 * (b1.Position.x * nx + b1.Position.y * ny - b2.Position.x * nx - b2.Position.y * ny) / (b1.Mass + b2.Mass);

						float vx1 = b1.Velocity.x + p * b1.Mass * nx;
						float vy1 = b1.Velocity.y + p * b1.Mass * ny;
						float vx2 = b2.Velocity.x - p * b2.Mass * nx;
						float vy2 = b2.Velocity.y - p * b2.Mass * ny;

						Vector3 newVel1 = new Vector3(vx1, vy1, 0f);
						b1.Velocity = newVel1;
						Vector3 newVel2 = new Vector3(vx2, vy2, 0f);
						b2.Velocity = newVel2;
					}
				}
			}
		}
	}

	private void ClearForces()
	{
		foreach (VerletBall b in Balls)
		{
			b.ClearForce();
		}
	}

	private void ProcessInput()
	{
		float horiz = Input.GetAxis("Horizontal");

		Balls[0].AddForce(Vector3.right * horiz * Balls[0].Mass * 10f);
	}

	private void UpdateVerletBall(VerletBall b)
	{
		if (b != null)
		{
			b.AddForce(Vector3.down * Gravity * b.Mass);
			//CheckCollision(b);
			b.PhysicsUpdate(1f / Timestep);
		}
	}

	private void UpdateSpring(Spring s)
	{
		if (s != null)
		{
			s.PhysicsUpdate(1f / Timestep);
		}
	}

	private void CheckCollision(VerletBall b)
	{
		Vector3 N = new Vector3(0f, 1f, 0f);
		float vRel = Vector3.Dot(N, b.Velocity);

		if (b.Position.y <= b.Radius && vRel < -0.001f)
		{
			Vector3 newVel = b.Velocity - N * (1f + b.Elasticity) * vRel;
			Vector3 p = b.Position;
			p.y = b.Radius;
			b.Position = p;

			b.Velocity = newVel;
		}
	}
}
