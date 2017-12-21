using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public bool inUse = false, canMove = false;
    public bool shootingRangeEnemy = false;
    public int direction;
    public Vector3 enemySpawnPos;
    public float enemySpeed = 7;

    private EnemyManager em;
    private UIManager UiMan;
    private float moveDist = 0;

    public void Spawning()
    {
        inUse = true;
    }
    public void OnEnable()
    {
        UiMan = GameObject.FindObjectOfType<UIManager>();
        shootingRangeEnemy = UiMan.shootRangeMode;
        em = GameObject.FindObjectOfType<EnemyManager>();
        if (shootingRangeEnemy)
        {
            moveDist = Random.Range(transform.position.x + 4, transform.position.x + 15);
            enemySpawnPos = this.gameObject.transform.position;
            canMove = true;
        }
        else
        {
            if(UiMan.shootRangeMode.Equals(false))
                StartCoroutine(StartEnemyLife(em.enemyLifeTime));
        }
    }
    private void LateUpdate()
    {
        if (canMove && inUse)
        {
            EnemyMovement();
            //canMove = false;
            //if(canMove.Equals(false))
            //    StartCoroutine(WaitShooty(Random.Range(.0005f, .001f)));// this gives it an arcadey feel
        }
    }
    private void EnemyMovement()
    {
        this.transform.position = new Vector3(Mathf.PingPong(Time.time * enemySpeed, moveDist), 0, transform.position.z);
    }
    private IEnumerator WaitShooty(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        canMove = true;
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
