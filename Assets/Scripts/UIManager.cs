using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

	//Game SetUp
	//public PlayerMovement playerStats;
	//public GameObject playerPre;
	//public GameObject player;
	//public GameObject startSquare;
	//GameObject[] barriersToDestroy;
	//public GameObject barrier;
	//public Vector3 spawnOffset;
	//public Vector3 spawnRot;
	//public int barrierCount;


	//HUD
	public int score = 0;
	public GameObject scoreText;
	public GameObject lastScore;
	public GameObject HUD;
	public GameObject splash;
	public bool gameStart;
	public float delay;
	public GameObject countDown;
	public GameObject celebrationText;
	public GameObject first;
	public GameObject second;
	public GameObject third;
	public GameObject hudFirst;
	public GameObject hudSecond;
	public GameObject hudThird;
	public int firstScore;
	public int secondScore;
	public int thirdScore;
    public bool shootRangeMode = false;

    private EnemyManager em;

    // Use this for initialization
    void Start()
	{
        em = GameObject.FindObjectOfType<EnemyManager>();
        countDown.GetComponent<Text>().text = "(Press Any Arrow To Start)";
		//try
		//{
		//	player = GameObject.FindGameObjectWithTag("Player");
		//}
		//catch
		//{
		//	player = null;
		//}
		//Restart();
	}

	// Update is called once per frame
	void Update()
	{
		if (!gameStart)
		{
			if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
			{
				StartCoroutine(StartDelay());
			}
		}
		//if (player == null)
		//{
		//	Restart();
		//}
		scoreText.GetComponent<Text>().text = score.ToString();
	}
	void Restart()
	{
		gameStart = false;
		HUD.SetActive(false);
		splash.SetActive(true);
		CheckScores();
		score = 0;
		//if (player != null)
		//{
		//	Destroy(player);
		//}
		//if (player == null)
		//{
		//	barriersToDestroy = GameObject.FindGameObjectsWithTag("Barrier");
		//	foreach (GameObject b in barriersToDestroy)
		//	{
		//		Destroy(b);
		//	}
		//	for (int i = 0; i <= barrierCount; i++)
		//	{
		//		Instantiate(barrier, new Vector3(0, 0, 600 * i), transform.rotation);
		//	}
		//	player = Instantiate(playerPre, startSquare.transform.position + spawnOffset, startSquare.transform.rotation) as GameObject;
		//}
	}
    //toggle
    public void SetMode()
    {
        shootRangeMode = !shootRangeMode;
    }
	public IEnumerator StartDelay()
	{
		//yield return new WaitForSeconds (delay);

		countDown.GetComponent<Text>().text = "3";
		yield return new WaitForSeconds(1);
		countDown.GetComponent<Text>().text = "2";
		yield return new WaitForSeconds(1);
		countDown.GetComponent<Text>().text = "1";
		yield return new WaitForSeconds(1);
		countDown.GetComponent<Text>().text = "GO!";
		HUD.SetActive(true);
		splash.SetActive(false);
		countDown.GetComponent<Text>().text = "(Press Any Arrow To Start)";
        //Debug.Log(shootRangeMode);
        em.StartGame(shootRangeMode);
		gameStart = true;
	}
	void CheckScores()
	{
		lastScore.GetComponent<Text>().text = score.ToString();
		if (score > firstScore)
		{
			celebrationText.SetActive(true);
			firstScore = score;
		}
		else if (score > secondScore)
		{
			celebrationText.SetActive(true);
			secondScore = score;
		}
		else if (score >= thirdScore)
		{
			celebrationText.SetActive(true);
			thirdScore = score;
		}
		else
		{
			celebrationText.SetActive(false);
		}
		first.GetComponent<Text>().text = firstScore.ToString();
		hudFirst.GetComponent<Text>().text = firstScore.ToString();
		second.GetComponent<Text>().text = secondScore.ToString();
		hudSecond.GetComponent<Text>().text = secondScore.ToString();
		third.GetComponent<Text>().text = thirdScore.ToString();
		hudThird.GetComponent<Text>().text = thirdScore.ToString();
	}
}
