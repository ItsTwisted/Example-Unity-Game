using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour {

	// Public variables (visible in editor)
	public float damage = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Called when trigger collides with another collider
	void OnTriggerStay2D(Collider2D other)
	{
		// Attempt to get the Player script from the thing we collided with
		Player playerScript = other.GetComponent<Player> ();

		// If the player script exists (aka it was actualy on the thing we collided with)...
		if (playerScript != null) {
			// Call the Damage function on the Player script and pass our damage variable as the amount.
			playerScript.Damage (damage);

			Debug.Log ("Goomba dealt damage to player = " + damage);
		} // end if (playerScript != null)
	} // end OnTriggerEnter2D()
}
