using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalFlower : Flower
{

    public static List<NormalFlower> normalFlowers = new List<NormalFlower>();

    void Awake()
    {
        normalFlowers.Add(this);
    }
}
