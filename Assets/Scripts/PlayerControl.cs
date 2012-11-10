// david deckman coss

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour {
	
	public float MaxForce = 0.7f;
	public float InertiaBalance = -.35f;
	public ForceMode Force = ForceMode.VelocityChange;
	
	public bool LockHorizontal = true;
	
	private Vector2 directionVector;
	
	void Start(){
		
	}
	
	void Update(){
		//get direction vector from input
		directionVector = new Vector2((LockHorizontal) ? 0 : Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		//apply new direction
		rigidbody.AddForce(directionVector * MaxForce, Force);
		
		//stop after keyup
		if (directionVector == new Vector2(0, 0)) rigidbody.AddForce(rigidbody.velocity * InertiaBalance, ForceMode.VelocityChange);
		
	}
}
