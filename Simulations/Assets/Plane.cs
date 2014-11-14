using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
public class Plane : MonoBehaviour
{
	public float Friction = 0.8f;
	public Vector3 Normal { get; private set; }

	private Mesh _mesh;

	public void Awake()
	{
		_mesh = GetComponent<MeshFilter>().mesh;
	}

	public void Update()
	{
		Friction = Mathf.Clamp(Friction, 0.0f, 1.0f);
		if (Normal != transform.up)
		{
			Normal = transform.up;
		}
	}

	public void HandleCollision(VerletBall ball)
	{
		float vRel = Vector3.Dot(Normal, ball.Velocity);
		Vector3 v = ball.Position - transform.position;
		float proj = Vector3.Dot(v, Normal);

		float pen = ball.Radius - proj;
		if (pen >= 0.0f)
		{
			if (vRel < -1f)//vi är på väg in i planet med fart (notera att den SMALLTHRESHOLD var för liten)
			{
				Vector3 newVelocity = ball.Velocity - Normal * (1f + ball.Elasticity) * vRel;
				ball.Velocity = newVelocity;
			}
			else if (vRel < 1)// VILANDE KONTAKT
			{
				Vector3 moveUp = Normal * pen;
				ball.Position += moveUp;
				ball.Velocity -= Normal * vRel;
				//all rörelse är nu i planet så vi kan motverka den
				ball.AddForce(ball.Velocity * -10f * ball.Mass * Friction);//OBS. inte fysikaliskt korrekt friktion
				
			}
			else
			{//vi är på väg ur planet med fart

			}
		}

	}
}
