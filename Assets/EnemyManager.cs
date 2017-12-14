using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour {

    public int misses, maxMisses, maxActive, currActive;
    public float delayBetweenMove, enemyLifeTime;
    public List<EnemyScript> gameObjectPos = new List<EnemyScript>();
    public bool shootingRangeMode = false;
	public int playerPoints, enemyPtValue;
	UIManager UiMan;

	private void Awake()
	{
		UiMan = GameObject.FindObjectOfType<UIManager>();
	}
    private void Start()
    {
        currActive = 0;
        if (shootingRangeMode)
            ShootingRangeMode();
        else
            SpawnWhackAMoleMode();
    }
    private void ShootingRangeMode()
    {

    }
    public void SpawnWhackAMoleMode()
    {
        for(int i = currActive; i < maxActive; i++)
        {
            int j = Random.Range(0, gameObjectPos.Count - 1);
            if (!gameObjectPos[j].inUse)
            {
                SpawnEnemy(gameObjectPos[j]);
            }
            else
            {
                foreach(EnemyScript _es in gameObjectPos)
                {
                    if (!_es.inUse)
                    {
                        SpawnEnemy(_es);
                        break;
                    }
                }
            }
            //Debug.Log(currActive);
        }
    }
    public void SpawnEnemy(EnemyScript _enemy)
    {
        if (!_enemy.gameObject.activeSelf)
        {
            _enemy.gameObject.SetActive(true);
            Debug.Log(_enemy.gameObject.name);
			_enemy.Spawning();
        }
        currActive++;
    }
	public void Despawn(EnemyScript _enemy)
	{
		currActive--;
		_enemy.inUse = false;
		_enemy.gameObject.SetActive(false);
		UiMan.score = (UiMan.score + enemyPtValue);

	}
    public void Update()
    {

    }
}
