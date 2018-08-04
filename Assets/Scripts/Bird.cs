using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{

    public float repulseForce;
    public LayerMask repulseLayer;
    public float repulseDistance;

    Vector2 _checkDirection = Vector2.up;

	void Update ()
	{
	    _checkDirection = Quaternion.AngleAxis(.45f, Vector3.forward)*_checkDirection;


	}
}
