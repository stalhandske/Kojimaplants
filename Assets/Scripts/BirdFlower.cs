using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFlower : Flower
{

    public GameObject birdPrefab;

    void Awake()
    {
        Instantiate(birdPrefab, transform.position+Vector3.up*.5f, Quaternion.identity);
    }
}
