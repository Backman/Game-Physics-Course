using UnityEngine;
using System.Collections;

public class Sphere : MonoBehaviour
{
	public float MovementSpeed = 1.0f;
	public float Mass = 0.2f;
	public float Dampening = 0.3f;

	public Transform transform { get; private set; }

	public Vector3 Force { get; private set; }
	public Vector3 Velocity { get; set; }
	public float Radius { get; private set; }

	private float _invMass;
	private float _oldMass;


	void Awake()
	{
		transform = base.transform;
	}

	void Start()
	{
		UpdateMass();
	}

	private void UpdateMass()
	{
		if(Mass != 0.0f)
		{
			_invMass = 1.0f / Mass;
		}
		else
		{
			_invMass = float.MaxValue;
		}

		_oldMass = Mass;
	}

	void Update()
	{
		Radius = Mathf.Max(transform.localScale.x, transform.localScale.y) / 2.0f;
		if(_oldMass != Mass)
		{
			UpdateMass();
		}

		float horiz = Input.GetAxis("Horizontal");
		if(horiz < 0.0f)
		{
			addForce(Vector3.left * MovementSpeed);
		}
		else if(horiz > 0.0f)
		{
			addForce(Vector3.right * MovementSpeed);
		}
	}

	public void PhysicsUpdate(float dt)
	{
		Vector3 deltaPos = Velocity * dt;
		transform.position += deltaPos;

		Vector3 deltaVel = Force * _invMass * dt;
		Velocity += deltaVel;
	}

	public void ClearForce()
	{
		Force = Vector2.zero;
	}

	public void addForce(Vector3 force)
	{
		Force += force;
	}
}
