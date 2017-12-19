using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public bool inUse = false;
    public bool shootingRangeEnemy = false;
    private EnemyManager em;

    public void Spawning()
    {
        inUse = true;
    }
    public void OnEnable()
    {
        em = GameObject.FindObjectOfType<EnemyManager>();
        StartCoroutine(StartEnemyLife(em.enemyLifeTime));
    }
    private IEnumerator StartEnemyLife(float _enemyLifeSpan)
    {
        yield return new WaitForSeconds(_enemyLifeSpan);
        if (inUse)
        {
            inUse = false;
            //Debug.Log("enemy in use, turning off now");
            em.misses++;
            Debug.Log(em.misses + " misses");
            em.Despawn(this.GetComponent<EnemyScript>());
        }
    }
}
