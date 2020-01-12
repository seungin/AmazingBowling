using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallShooter : MonoBehaviour
{
	public Rigidbody bullet;
	public Transform gunpoint;
	public Slider powerGauge;

	public float minForce = 15f;
	public float maxForce = 30f;
	private float targetForce;

	public float chargingTime = 1f;
	private float chargingSpeed;

	private void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			targetForce = minForce;
		}
		else if (Input.GetButton("Fire1"))
		{
			chargingSpeed = (maxForce - minForce) / chargingTime;
			targetForce += chargingSpeed * Time.deltaTime;
			powerGauge.value = targetForce;
		}
		else if (Input.GetButtonUp("Fire1"))
		{
			Rigidbody instance = Instantiate(bullet, gunpoint.position, gunpoint.rotation);
			instance.velocity = targetForce * gunpoint.forward;
			powerGauge.value = minForce;
		}
	}
}
