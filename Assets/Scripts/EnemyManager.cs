using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour {

    public int misses, maxMisses, maxActive, currActive;
    public float spawnDelay, enemyLifeTime;
    public List<EnemyScript> gameObjectPos = new List<EnemyScript>();
    public bool shootingRangeMode = false;
	public int playerPoints, enemyPtValue;

    private UIManager UiMan;

	private void Awake()
	{
		UiMan = GameObject.FindObjectOfType<UIManager>();
	}
    private void Start()
    {
        currActive = 0;
    }
    public void StartGame(bool _shootingRange)
    {
        if (_shootingRange)
        {
            shootingRangeMode = true;
            StartCoroutine(UiMan.StartDelay());
            ShootingRangeMode();
        }
        else
        {
            shootingRangeMode = false;
            StartCoroutine(UiMan.StartDelay());
            SpawnWhackAMoleMode();
        }
    }
    private void ShootingRangeMode()
    {
        Debug.Log("Start shooting range");
        GenSpawn();
    }
    //made it up with spawning seperately so going to call another method in here
    private void SpawnWhackAMoleMode()
    {
        GenSpawn();
        //for(int i = currActive; i < maxActive; i++)
        //{
        //    int j = Random.Range(0, gameObjectPos.Count - 1);
        //    if (!gameObjectPos[j].inUse)
        //    {
        //        SpawnEnemy(gameObjectPos[j]);
        //    }
        //    else
        //    {
        //        foreach(EnemyScript _es in gameObjectPos)
        //        {
        //            if (!_es.inUse)
        //            {
        //                SpawnEnemy(_es);
        //                break;
        //            }
        //        }
        //    }
        //}
    }
    //look at spawn whack a mole mode comment for explanaition
    private void GenSpawn()
    {
        for (int i = currActive; i < maxActive; i++)
        {
            int j = Random.Range(0, gameObjectPos.Count - 1);
            if (!gameObjectPos[j].inUse)
            {
                SpawnEnemy(gameObjectPos[j]);
            }
            else
            {
                foreach (EnemyScript _es in gameObjectPos)
                {
                    if (!_es.inUse)
                    {
                        SpawnEnemy(_es);
                        break;
                    }
                }
            }
        }
    }
    public IEnumerator WaitSpawn(float _wait, EnemyScript _enemy)
    {
        yield return new WaitForSeconds(_wait);
        if(currActive < maxActive)
        {
            SpawnWhackAMoleMode();
        }
    }
    private void SpawnEnemy(EnemyScript _enemy)
    {
        if (!_enemy.gameObject.activeSelf)
        {
            _enemy.gameObject.SetActive(true);
            //Debug.Log(_enemy.gameObject.name);
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
        if (misses >= maxMisses)
        {
            EndGame();
        }
        else
        {
            StartCoroutine(WaitSpawn(spawnDelay, _enemy));
        }
    }
    public void EndGame()
    {
        Debug.Log("end game");
        UiMan.gameStart = false;
        SceneManager.LoadScene("GameOver");
    }
}
