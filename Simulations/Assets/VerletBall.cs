using UnityEngine;
using System.Collections;

public class VerletBall : MonoBehaviour 
{
	public float Mass = 0.5f;
	public float Elasticity = 1.0f;

	public Vector3 Force { get; private set; }
	public float Radius { get; private set; }

	private float _invMass;
	private float _oldMass;
	private float _radius;
	public Vector3 Position
	{
		get
		{
			return transform.position;
		}
		set
		{
			transform.position = value;
		}
	}
	public Vector3 Velocity
	{
		get
		{
			return (Position - _prevPos) * VerletPhysicsManager.Instance.Timestep;
		}
		set
		{
			_prevPos = Position - value * (1f / VerletPhysicsManager.Instance.Timestep);
		}
	}

	private Vector3 _prevPos;

	public void Start()
	{
		Velocity = Vector3.zero;
	}

	public void Update()
	{
		Radius = Mathf.Max(transform.localScale.x, transform.localScale.y) / 2.0f;
		Mass = Mathf.Clamp(Mass, 0.1f, float.MaxValue);
		Elasticity = Mathf.Clamp(Elasticity, 0.1f, 1.0f);
		if (_oldMass != Mass)
		{
			SetMass(Mass);
		}
	}

	private void SetMass(float mass)
	{
		Mass = mass;

		if (Mass != 0.0f)
		{
			_invMass = 1.0f / Mass;
		}
		else
		{
			_invMass = float.MaxValue;
		}

		_oldMass = mass;
	}

	public void ClearForce()
	{
		Force = Vector3.zero;
	}

	public void AddForce(Vector3 force)
	{
		Force += force;
	}

	public float DistSqr(VerletBall other)
	{
		return (Position - other.Position).sqrMagnitude;
	}

	public float Dist(VerletBall other)
	{
		return (Position - other.Position).magnitude;
	}

	public void PhysicsUpdate(float dt)
	{
		float step = 1f / VerletPhysicsManager.Instance.Timestep;
		Vector3 newPos = Position * 2f - _prevPos + Force * _invMass * step * step;
		_prevPos = Position;
		Position = newPos;
	}
}
