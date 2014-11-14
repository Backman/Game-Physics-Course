using UnityEngine;
using System.Collections;

public class Spring : MonoBehaviour
{
	public Transform FixedPoint;
	public VerletBall Ball;
	public float DampingCoefficient = 1f;
	public float SpringConstant = 2f;
	public float RestLength = 2f;

	private LineRenderer _lineRenderer;

	public void Awake()
	{
		_lineRenderer = gameObject.AddComponent<LineRenderer>();
		_lineRenderer.material = Resources.Load<Material>("SpringMaterial");
		_lineRenderer.SetWidth(0.5f, 0.5f);
	}

	public void Update()
	{
		if (FixedPoint != null && Ball != null)
		{
			_lineRenderer.SetVertexCount(2);
			_lineRenderer.SetPosition(0, FixedPoint.position);
			_lineRenderer.SetPosition(1, Ball.Position);
		}
		else
		{
			_lineRenderer.SetVertexCount(0);
		}

		DampingCoefficient = Mathf.Clamp(DampingCoefficient, 0.05f, 10f);
		RestLength = Mathf.Clamp(RestLength, 0.0f, float.MaxValue);
	}

	public void PhysicsUpdate(float dt)
	{
		if (FixedPoint != null && Ball != null)
		{
			Vector3 x = FixedPoint.position - Ball.Position;
			float length = x.magnitude;
			Vector3 dir = x.normalized;

			// F = k(|x|-d)(x/|x|) - bv
			Vector3 force = SpringConstant * (length - RestLength) * dir;
			force -= DampingCoefficient * Ball.Velocity;

			Ball.AddForce(force);
		}
	}
}
