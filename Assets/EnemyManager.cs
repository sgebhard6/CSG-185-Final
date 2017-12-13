using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public int misses, maxMisses, maxActive, currActive;
    public float delayBetweenMove;
    public List<EnemyScript> gameObjectPos = new List<EnemyScript>();
    public bool shootingRangeMode = false;

    private void Awake()
    {
        currActive = 0;
        if (shootingRangeMode)
            ShootingRangeMode();
        else
            WhackAMoleMode();
    }
    private void ShootingRangeMode()
    {

    }
    private void WhackAMoleMode()
    {
        for(int i = currActive; i < maxActive; i++)
        {
            int j = Random.Range(0, gameObjectPos.Count - 1);
            if (!gameObjectPos[j].inUse)
            {
                SpawnEnemy(gameObjectPos[j].gameObject);
            }
            else
            {
                foreach(EnemyScript _es in gameObjectPos)
                {
                    if (!_es.inUse)
                    {
                        SpawnEnemy(_es.gameObject);
                        break;
                    }
                }
            }
            Debug.Log(currActive);
        }
    }
    public void SpawnEnemy(GameObject _enemy)
    {
        if (_enemy.activeSelf.Equals(false))
            _enemy.SetActive(true);
        Debug.Log(_enemy.gameObject.name);
        currActive++;
    }
   
    public void Update()
    {

    }
    
}
