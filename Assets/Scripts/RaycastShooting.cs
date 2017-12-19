using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastShooting : MonoBehaviour {

	EnemyManager eMAn;

	void OnEnable () {
		eMAn = GameObject.FindObjectOfType<EnemyManager>();
	}

	void Update () {
		if (Input.GetButtonDown("Fire1"))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			//Debug.Log("firing");
			if (Physics.Raycast(ray,out hit, 100))
			{
				//Debug.Log("raycasting");
				if (hit.collider.GetComponent<EnemyScript>())
				{
					//Debug.Log("hit");
					eMAn.Despawn(hit.collider.GetComponent<EnemyScript>());
					hit.collider.gameObject.SetActive(false);
				}
			}
		}
	}
}
