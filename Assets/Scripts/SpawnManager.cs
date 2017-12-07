using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	public int missesInARow = 0;
	public List<GameObject> positionsList = new List<GameObject>();
	public int maxNumActive;
	
	public List<GameObject> occupiedPositions = new List<GameObject>();

	private bool gameOver = false, shootyGame = false;//shootyGame is if the game type is shooting or whack a mole
	private bool needSpawns = false;
	//private bool isMoveable = false; may not need from the check in spawn enemies

	void Start () {
		
	}
	private void StartGame()
	{
		int i = HowManyToActivate(maxNumActive);
		for (int j = 0; j < i; j++)
		{
			SpawnEnemy(Random.Range(0, positionsList.Count-1));
		}
	}
	private void SpawnEnemy(int _index)
	{
		Debug.Log(_index);
		//make a class on the enemies that contains a public bool
		//the bool will determine if the enemy can move or not
		//check if the enemy can move
		//determine game mode from available enemy types
	}
	private int HowManyToActivate(int _maxAlive)
	{
		int _count = Random.Range(1, _maxAlive);
		return _count;
	}
	void Update () {
		
	}
}
