using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	public Transform carTransform;
	[Range(1, 10)]
	public float followSpeed = 2;
	[Range(1, 10)]
	public float lookSpeed = 5;
	Vector3 initialCameraPosition;
	Vector3 initialCarPosition;
	Vector3 absoluteInitCameraPosition;

	void Start()
	{
		// Auto-find the car if not assigned
		if (carTransform == null)
		{
			GameObject car = GameObject.Find("Prometheus");
			if (car != null)
			{
				carTransform = car.transform;
			}
		}

		if (carTransform != null)
		{
			initialCameraPosition = gameObject.transform.position;
			initialCarPosition = carTransform.position;
			absoluteInitCameraPosition = initialCameraPosition - initialCarPosition;
		}
	}

	void FixedUpdate()
	{
		// Ensure we have a car to follow
		if (carTransform == null)
		{
			GameObject car = GameObject.Find("Prometheus");
			if (car != null)
			{
				carTransform = car.transform;
			}
			else
			{
				return; // No car found, skip this frame
			}
		}

		// NFS Style: Camera follows behind the car with higher view
		Vector3 _targetPos = carTransform.position + (carTransform.forward * -8f) + (carTransform.up * 6f);
		transform.position = Vector3.Lerp(transform.position, _targetPos, followSpeed * Time.deltaTime);

		// Look at the car (slightly ahead for better view)
		Vector3 _lookTarget = carTransform.position + (carTransform.forward * 2f);
		Vector3 _lookDirection = _lookTarget - transform.position;
		Quaternion _rot = Quaternion.LookRotation(_lookDirection, Vector3.up);
		transform.rotation = Quaternion.Lerp(transform.rotation, _rot, lookSpeed * Time.deltaTime);

	}

}
