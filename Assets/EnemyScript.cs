using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    public bool inUse = false;
    public bool shootingRangeEnemy = false;

    public void Spawning()
    {
        inUse = true;
    }
}
