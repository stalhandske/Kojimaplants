using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PufMaker : MonoBehaviour
{

    public GameObject puffPrefab;

    public void Puff()
    {
        Instantiate(puffPrefab, transform.position, Quaternion.identity);
    }
}
