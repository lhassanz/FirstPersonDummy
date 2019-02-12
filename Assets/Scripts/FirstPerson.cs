﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPerson : MonoBehaviour {


	public float speed;
	public float jumpHeight;
	public LayerMask ground;
	public Transform feet;

	private Vector3 direction;
	private Rigidbody rbody;

	private float rotationSpeed = 1f;
	private float rotationY = 10f;
	private float rotationX = 0f;

	public GameObject bulletPrefab;
	public Transform bulletSpawn; 

	// Use this for initialization
	void Start () {
		speed = 4.0f;
		jumpHeight = 3.0f;
		rbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		direction = Vector3.zero;
		direction.x = Input.GetAxis("Horizontal");
		direction.z = Input.GetAxis("Vertical");
		direction = direction.normalized;
		if(direction.x != 0) {
			rbody.MovePosition(rbody.position + transform.right * direction.x * speed * Time.deltaTime);
		}
		if(direction.z != 0){
			rbody.MovePosition(rbody.position + transform.forward * direction.z * speed * Time.deltaTime);
		}

		rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * rotationSpeed;
		rotationY += Input.GetAxis("Mouse Y") * rotationSpeed;
		transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);

		bool isGrounded = Physics.CheckSphere(feet.position, 0.1f, ground, QueryTriggerInteraction.Ignore);

		if(Input.GetButtonDown("Jump") && isGrounded) {
			rbody.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
		}

		if(Input.GetButtonDown("Fire1")){
			Fire();
		}
	}

	void Fire(){
		var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;
		Destroy(bullet, 4.0f);
	}
}
