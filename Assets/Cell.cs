using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {

	public bool alive;
	public int neighbour = 0;

	public void CheckAlive()
	{
		if (neighbour == 3 || neighbour == 2)
		{
			alive = true;
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
