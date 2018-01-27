using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
	public delegate void EndGameDelegate ();

	public static event EndGameDelegate OnEndGame;

	public int misses, maxMisses, maxActive, currActive;
	public float spawnDelay, enemyLifeTime;
	public List<EnemyScript> gameObjectPos = new List<EnemyScript> ();
	public bool shootingRangeMode = false;
	public int playerPoints, enemyPtValue;

	private UIManager UiMan;
	private GetSprites _getSprites;

	bool _shootingRange;

	void OnEnable ()
	{
		GetSprites.OnGetSprites += SetSprites;
	}

	void OnDisable ()
	{
		GetSprites.OnGetSprites -= SetSprites;
	}

	private void Awake ()
	{
		UiMan = GameObject.FindObjectOfType<UIManager> ();
		_getSprites = GetComponent <GetSprites> ();
	}

	private void Start ()
	{
		currActive = 0;
	}

	void SetSprites ()
	{
		for (int i = 0; i < gameObjectPos.Count; i++) {
			gameObjectPos [i].gameObject.GetComponent<SpriteRenderer> ().sprite = _getSprites.spriteList [0];
		}
	}

	public void SetGameType (string GameTypeName)
	{
		switch (GameTypeName) {
		case "WhackAMole":
			_shootingRange = false;
			UiMan.shootRangeMode = false;
			break;
		case "ShootingRange":
			_shootingRange = true;
			UiMan.shootRangeMode = true;
			break;
		default:
			break;
		}
	}

	public void StartGame ()
	{
		if (_shootingRange) {
			shootingRangeMode = _shootingRange;
			StartCoroutine (UiMan.StartDelay ());
			ShootingRangeMode ();
		} else {
			shootingRangeMode = _shootingRange;
			StartCoroutine (UiMan.StartDelay ());
			SpawnWhackAMoleMode ();
		}
	}

	private void ShootingRangeMode ()
	{
		GenSpawn ();
	}
	//made it up with spawning seperately so going to call another method in here
	private void SpawnWhackAMoleMode ()
	{
		GenSpawn ();

	}
	//look at spawn whack a mole mode comment for explanaition
	private void GenSpawn ()
	{
		for (int i = currActive; i < maxActive; i++) {
			int j = Random.Range (0, gameObjectPos.Count - 1);
			if (!gameObjectPos [j].inUse) {
				SpawnEnemy (gameObjectPos [j]);
			} else {
				foreach (EnemyScript _es in gameObjectPos) {
					if (!_es.inUse) {
						SpawnEnemy (_es);
						break;
					}
				}
			}
		}
	}

	public IEnumerator WaitSpawn (float _wait, EnemyScript _enemy)
	{
		yield return new WaitForSeconds (_wait);
		if (currActive < maxActive) {
			SpawnWhackAMoleMode ();
		}
	}

	private void SpawnEnemy (EnemyScript _enemy)
	{
		if (!_enemy.gameObject.activeSelf) {
			_enemy.gameObject.SetActive (true);
			_enemy.Spawning ();
		}
		currActive++;
	}

	public void Despawn (EnemyScript _enemy, int _pts)
	{
		currActive--;
		_enemy.inUse = false;
		_enemy.gameObject.SetActive (false);
		UiMan.score = (UiMan.score + _pts);
		if (misses >= maxMisses) {
			EndGame ();
		} else {
			StartCoroutine (WaitSpawn (spawnDelay, _enemy));
		}
	}

	public void EndGame ()
	{
		UiMan.gameStart = false;
		OnEndGame ();
	}
}
