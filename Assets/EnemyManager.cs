using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public int misses, maxMisses, maxActive, currActive;
    public float delayBetweenMove, enemyLifeTime;
    public List<EnemyScript> gameObjectPos = new List<EnemyScript>();
    public bool shootingRangeMode = false;

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
            try
            {
                _enemy.Spawning();
            }
            catch
            {
                Debug.LogError("Spawn enemy cannot access the enemy's script");
            }
        }
        //currActive++;
    }
   
    public void Update()
    {

    }
}
