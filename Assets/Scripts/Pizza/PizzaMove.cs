﻿using UnityEngine;
using System.Collections;


public class PizzaMove : MonoBehaviour {

    Rigidbody2D rg2d;
	PizzaInventory pi;

	// Use this for initialization
	void Start () {
        rg2d = GetComponent<Rigidbody2D>();
		pi = GetComponent<PizzaInventory>();
	}
	
	// Update is called once per frame
	void Update () {

	    
	}
    void FixedUpdate()
    {
        if ( rg2d.velocity.magnitude > 0.5f)
            rg2d.MoveRotation(Angle360(Vector2.right, rg2d.velocity));
		rg2d.AddForce (Friction (rg2d.velocity));
	}

    float Angle360(Vector2 from, Vector2 to)
    {
        // only returns from 0 to 180
        float angle = Vector2.Angle(from, to);
        Vector3 cross = Vector3.Cross(from, to);
        if (cross.z < 0)
            angle = 360 - angle;
        return angle;
    }

	Vector2 Friction(Vector2 velocity)
	{
		float magnitude = velocity.magnitude;
        //Debug.Log ("speed " + magnitude);
        if (magnitude < 15f)
            return Vector2.zero;
        else if (magnitude < 40f)
            return -velocity * 0.05f;
        else if (magnitude < 70f)
            return -velocity * 0.2f;
        else if (magnitude < 200f)
            return -velocity * 0.5f;
        else
            return -velocity * 0.99f;
        //throw new UnityException ("moving too fast above 200f");
	}

	// Handle Player hitting objects with Colliders
	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Asteroid") {
			pi.Decrement ("Pizza");
			//FlipVelocity ();
			SlowInstantly (0.8f);
			Destroy (other.gameObject);
		}
	}

	void SlowInstantly(float factor)
	{
		rg2d.AddForce (-rg2d.velocity * rg2d.mass * factor,ForceMode2D.Impulse);
	}

	void FlipVelocity()
	{
		rg2d.velocity = -rg2d.velocity;
	}
}
