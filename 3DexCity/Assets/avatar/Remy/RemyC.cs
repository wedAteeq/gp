using UnityEngine;
using System.Collections;

public class RemyC : MonoBehaviour {

	Animator anim;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		move();
		sit ();
		carry ();
	}
	void move(){
		
		anim.SetFloat ("Walk", Input.GetAxis("Vertical"));

		if(Input.GetKey (KeyCode.LeftShift))
			anim.SetFloat ("jump", 1);
		else
			anim.SetFloat ("jump", 0);
		//_______________________________________
		if(Input.GetKey (KeyCode.RightArrow))
			anim.SetBool("turnRight", true);
		else
			anim.SetBool("turnRight", false);
		//_______________________________________
		if(Input.GetKey (KeyCode.LeftArrow))
			anim.SetBool("turnLeft", true);
		else
			anim.SetBool("turnLeft", false);
		//_______________________________________
		
	}
	void sit(){
		if (Input.GetKey (KeyCode.S))
			anim.SetInteger ("sit", 1);
		else
			anim.SetInteger ("sit", 0);
	}
	void carry (){
		if (Input.GetKey (KeyCode.C))
			anim.SetInteger ("carry", 1);
		else
			anim.SetInteger ("carry", 0);
	}
}
