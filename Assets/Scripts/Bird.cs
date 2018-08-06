using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{

    public float repulseForce;
    public LayerMask repulseLayer;
    public float repulseDistance;
    public float attractionToStartPos;
    public float attractionToSeeds;
    public float randomRoam;

    Vector2 _checkDirection = Vector2.up;
    Rigidbody2D _rigidbody2D;
    Vector2 _startPosition;
    Vector2 _randomDirection;
    Seed _nearestSeed = null;
    float _distanceToNearest = float.PositiveInfinity;
    Vector2 _positionOfNearestSeed = Vector2.zero;

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _startPosition = transform.position;
        _randomDirection = Random.insideUnitCircle.normalized;
    }

	void FixedUpdate ()
	{
	    _randomDirection = Quaternion.AngleAxis(Random.value, Vector3.forward)*_randomDirection;

	    _rigidbody2D.AddForce(randomRoam * _randomDirection * Time.deltaTime);
        
        _checkDirection = Quaternion.AngleAxis(30, Vector3.forward)*_checkDirection;

	    RaycastHit2D hit = Physics2D.Raycast(transform.position, _checkDirection, repulseDistance, repulseLayer);
	    if (hit)
	    {
            _rigidbody2D.AddForce(repulseForce*-_checkDirection*Time.deltaTime*(1-hit.distance/repulseDistance));
	    }

        _rigidbody2D.AddForce(attractionToStartPos*((Vector3)_startPosition - transform.position).normalized*Time.deltaTime);

	    if (!_nearestSeed)
	        _distanceToNearest = 9999999;
	    
	    foreach (Seed s in Seed.seeds)
	    {
	        Vector2 dir = (s.transform.position - transform.position);
	        float dist = dir.sqrMagnitude;
	        if (dist < _distanceToNearest)
	        {
	            _nearestSeed = s;
	            _distanceToNearest = dist;
	            _positionOfNearestSeed = s.transform.position;
	        }
	    }

        if (_nearestSeed)
    	    _rigidbody2D.AddForce(attractionToSeeds * ((Vector3)_positionOfNearestSeed - transform.position).normalized * Time.deltaTime);
        
	}
}
