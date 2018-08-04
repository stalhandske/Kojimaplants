using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{

    public float repulseForce;
    public LayerMask repulseLayer;
    public float repulseDistance;
    public float attractionToStartPos;

    Vector2 _checkDirection = Vector2.up;
    Rigidbody2D _rigidbody2D;
    Vector2 _startPosition;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _startPosition = transform.position;
    }

	void FixedUpdate ()
	{
	    _checkDirection = Quaternion.AngleAxis(30, Vector3.forward)*_checkDirection;

	    RaycastHit2D hit = Physics2D.Raycast(transform.position, _checkDirection, repulseDistance, repulseLayer);
	    if (hit)
	    {
            _rigidbody2D.AddForce(repulseForce*-_checkDirection*Time.deltaTime);
	    }

        _rigidbody2D.AddForce(attractionToStartPos*((Vector3)_startPosition - transform.position).normalized*Time.deltaTime);
	}
}
