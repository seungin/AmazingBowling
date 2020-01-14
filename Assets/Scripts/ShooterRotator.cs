using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterRotator : MonoBehaviour
{
	enum RotateState
	{
		Idle, Horizontal, Vertical, Ready
	};

	private RotateState state;

	public BallShooter ballShooter;
	public float rotateSpeed = 360f;

	private void OnEnable()
	{
		state = RotateState.Idle;
		ballShooter.transform.rotation = Quaternion.identity;
		ballShooter.enabled = false;
	}

	private void Update()
	{
		switch (state)
		{
			case RotateState.Idle:
				if (Input.GetButtonDown("Fire1"))
				{
					state = RotateState.Horizontal;
				}
				break;

			case RotateState.Horizontal:
				if (Input.GetButton("Fire1"))
				{
					transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
				}
				else if (Input.GetButtonUp("Fire1"))
				{
					state = RotateState.Vertical;
				}
				break;

			case RotateState.Vertical:
				if (Input.GetButton("Fire1"))
				{
					transform.Rotate(new Vector3(rotateSpeed * Time.deltaTime, 0, 0));
				}
				else if (Input.GetButtonUp("Fire1"))
				{
					state = RotateState.Ready;
					ballShooter.enabled = true;
				}
				break;

			default:
				break;
		}
	}
}
