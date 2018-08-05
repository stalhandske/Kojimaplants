using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puf : MonoBehaviour
{

    public float disappearTime;

	void Awake ()
	{
		Destroy(gameObject, disappearTime);
	}
}
