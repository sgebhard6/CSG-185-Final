using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour {

	public float enemyMoveSpeed;
	public List<GameObject> positionList = new List<GameObject>();
	//change to private later
	public bool wasHit = false;

	public virtual void Start()
	{

	}
	public virtual void Update()
	{

	}

	public void EnemyMovement()//movement pattern of the enemies
	{

	}
	public void EnemyDeath(bool _wasHit, GameObject _deadObject)//when enemy is hit
	{
		if (_wasHit.Equals(true))
			Debug.Log("send true message");
		else
			Debug.Log("send false message to spawner");
		//have to send a message to the enemy spawner
	}
}
