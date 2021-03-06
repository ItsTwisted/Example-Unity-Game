﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed = 5;
	public float jumpSpeed = 12;
	public float health = 100;
	public float invulnerableDuration = 1;
	public float blinkDuration = 0.5f;

	private float invulnerableEndTime = 0;
	private float blinkEndTime = 0;
	private bool hasDoubleJumped = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Rigidbody2D ourRigidBody = GetComponent<Rigidbody2D> ();

		//Get the current horizontal input (left/right arrows) - between -1 and 1.
		float horizontal = Input.GetAxis ("Horizontal");

		//Get the current velocity from the physics system
		Vector2 velocity = ourRigidBody.velocity;

		//Set our velocity based on the input and our speed value
		velocity.x = horizontal * speed;

		//Determine if touching ground
		//Get the collider attached to this object
		Collider2D ourCollider = GetComponent<Collider2D>();

		// Get the LayerMask for the ground layer - we need this for the next function call
		LayerMask groundLayer = LayerMask.GetMask("Ground");

		//Ask the collider if we are touching this layer
		bool isTouchingGround = ourCollider.IsTouchingLayers(groundLayer);

		// If we are touching the groun, reset our double jump
		if (isTouchingGround == true) {
			hasDoubleJumped = false;

		}

		// Normally only allowed to jump if touching the ground...
		bool allowedToJump = isTouchingGround;

		if(isTouchingGround == false && hasDoubleJumped == false)
			allowedToJump = true;


		//Jump logic
		bool jumpPressed = Input.GetButtonDown("Jump");

		//Only jump if we have both pressed the button AND are allowed to jump
		if (jumpPressed == true && allowedToJump == true) {
			//Apply velocity to jump
			velocity.y = jumpSpeed;

			//If we touched when we weren't on the ground, then we did a double jump
			if (isTouchingGround == false)
				hasDoubleJumped = true;
		}

		//Put this velocity back in the physics system
		ourRigidBody.velocity = velocity;


		// Handle blinking while invulnerable:

		// Get our sprite renderer component attached to this object
		SpriteRenderer renderer = GetComponent<SpriteRenderer> ();

		//flip the sprite on the x axis only if velocity x is less than 0
		renderer.flipX = velocity.x < 0;

		// Are we done being invulnerable?
		if (Time.time >= invulnerableEndTime) {
			// if NOT invulnerable...

			// Set the renderer to enabled.
			renderer.enabled = true;
		} else {
			// If YES invulnerable...

			// If it is time to blink...
			if (Time.time >= blinkEndTime) {
				// set our renderer enabled value to the opposite of what it currently is (toggle it)
				renderer.enabled = !renderer.enabled;
				// Set the next time we should blink to our current time plus the blink duration
				blinkEndTime = Time.time + blinkDuration;
			} // end if (Time.time >= blinkEndTime)
		} // end if (Time.time >= invulnerableEndTime)

		//Mouse test
		//Check if left mouse button has been pressed down THIS FRAME!!!
//		if (Input.GetMouseButtonDown (0) == true) {
//			Debug.Log ("Mouse left button just pressed down");
//			}
//		if (Input.GetMouseButton (0) == true) {
//			Debug.Log ("Mouse left button held");
//		}
//		if (Input.GetMouseButtonUp (0) == true) {
//			Debug.Log ("Mouse left button just released");
//		}
//		if (Input.GetMouseButtonDown (1) == true) {
//			Debug.Log ("Mouse right button just pressed down");
//		}
//
//		Debug.Log ("Mouse position = " + Input.mousePosition);
//
	} // end Update()

	public void Damage(float damageToDeal)
	{
		if (Time.time >= invulnerableEndTime) {

			// Reducing health by the damage passed in
			health = health - damageToDeal;

			// TODO: handle death

			// Set us as invulnerable for a set duration
			invulnerableEndTime = Time.time + invulnerableDuration;

			// Log the result of the function
			Debug.Log("Damage was dealt");
			Debug.Log("damageToDeal = "+damageToDeal);
			Debug.Log("health = "+health);
		}
	} // end Damage()
}
