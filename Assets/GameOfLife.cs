using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLife : MonoBehaviour {

	public GameObject cellPrefab;
	
	//Game settings
	public float tick;
	int gridSizeX = 40;
	int gridSizeY = 40;
	int spawnPercentage = 20;

	float nextTick = 0;
	Cell[,] cells;	//Saving cell instead of GameObject made it much easier.

	void Start () {
		//Game setup
		cells = new Cell[gridSizeX, gridSizeY];

		for (int x = 0; x < gridSizeX; x++)
		{
			for (int y = 0; y < gridSizeY; y++)
			{
				//Only spawn objects if they are alive.
				if(Random.Range(0, 100) < spawnPercentage)
				{
					Vector3 spawnOffset = new Vector3(x, 0, y);
					GameObject newCell = Instantiate(cellPrefab, transform.position + spawnOffset, Quaternion.identity, transform);
					cells[x, y] = newCell.GetComponent<Cell>();
					cells[x, y].alive = true;
				}
			}
		}
	}

	void Update () {
		//If enough time has passed since last tick
		if (nextTick > tick)
		{
			//Loop throuh all cells
			for (int x = 0; x < gridSizeX; x++)
			{
				for (int y = 0; y < gridSizeY; y++)
				{
					//Count neighbours on each location of our grid.
					int neighbour = 0;

					for (int dx = -1; dx <= 1; dx++)
					{
						for (int dy = -1; dy <= 1; dy++)
						{
							if (dx == 0 && dy == 0)
							{
								continue;
							}
							
							try
							{
								//Only count existing cells marked with alive.
								if(cells[x + dx, y + dy] != null && cells[x + dx, y + dy].alive)
								{
									neighbour++;
								}
							}
							catch (System.Exception)
							{		

							}
						}
					}

					//If we have 3 neighbours and doesn't exists, spawn a new object
					if(cells[x, y] == null && neighbour == 3)
					{
						Vector3 spawnOffset = new Vector3(x, 0, y);
						cells[x, y] = Instantiate(cellPrefab, transform.position + spawnOffset, transform.rotation, transform).GetComponent<Cell>();
					}

					if(cells[x, y] != null)
					{
						cells[x, y].neighbour = neighbour;
					}
				}
			}

			for (int x = 0; x < gridSizeX; x++)
			{
				for (int y = 0; y < gridSizeY; y++)
				{
					if(cells[x, y] != null)
						cells[x, y].CheckAlive();
				}
			}

			nextTick -= tick;
		}

		nextTick += Time.deltaTime;
	}
}
