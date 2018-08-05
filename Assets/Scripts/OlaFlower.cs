using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OlaFlower : Flower
{
    public GameObject olaPrefab;
    public Vector2 olaSpawnOffset;

    public void SpawnOla()
    {
        Instantiate(olaPrefab, transform.position + (Vector3)olaSpawnOffset, Quaternion.identity);
    }
}
