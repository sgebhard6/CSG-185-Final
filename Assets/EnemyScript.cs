using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public bool inUse = false;
    public bool shootingRangeEnemy = false;

    private bool move = false;
    private float timer;

    EnemyManager eMAn;

    private void Start()
    {
        eMAn = GameObject.FindObjectOfType<EnemyManager>();
    }
    public void Spawning()
    {
        inUse = true;
        eMAn.currActive++;
        timer = eMAn.enemyLifeTime;
        move = true;
    }
    private void Update()
    {
        if (move)
        {
            Debug.Log(this.gameObject.name + " move");
            //timer -= Time.deltaTime;
            if(timer <= 0)
            {
                //Despawning();
            }
        }
    }
    public void Despawning()
    {
        move = false;
        inUse = false;
        eMAn.currActive--;
        eMAn.SpawnWhackAMoleMode();
    }
}
