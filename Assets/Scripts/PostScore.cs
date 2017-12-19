using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PostScore : MonoBehaviour
{
	public int score;
	int playerID;
	int gameTypeID;
	public PostGameType pgt;

	public string targetURL;

	public List<TopTenScore> highScores;

	void OnEnable ()
	{
		PostPlayer.OnPost += SetPlayerID;
	}

	void OnDisable ()
	{
		PostPlayer.OnPost -= SetPlayerID;
	}

	void SetPlayerID (int _playerID)
	{
		playerID = _playerID;
		StartCoroutine (Post ());
	}

	IEnumerator Post ()
	{
		gameTypeID = pgt.GetGameTypeID ();
		print ("game id: " + gameTypeID);
		Score scoreToPost = new Score (playerID, gameTypeID, score);
		UnityWebRequest www = UnityWebRequest.Post ("http://localhost:83/api/Scores", "");
		UploadHandler customUploadHandler = new UploadHandlerRaw (System.Text.Encoding.UTF8.GetBytes (JsonUtility.ToJson (scoreToPost)));
		customUploadHandler.contentType = "application/json";
		www.uploadHandler = customUploadHandler;
		yield return www.Send ();
		if (www.error != null)
			Debug.Log ("Error: " + www.error);
		else
			Debug.Log ("Success: " + www.responseCode);

		StartCoroutine (GetScores ());
	}

	IEnumerator GetScores ()
	{
		highScores.Clear ();
		WWW response = new WWW (targetURL);
		yield return response;
		if (response.error != null)
			Debug.Log (response.error);
		else {
			//remove square brackets
			string responseString = response.text.Substring (1, response.text.Length - 2);
			//split on commas
			string[] substrings = responseString.Split ('{');
			//pass each string into from json
			int i = 0;
			foreach (string sub in substrings) {
				//store all of that data in a list of scores
				if (sub != "") {
					string tempString = "{" + sub;

					if (tempString [tempString.Length - 1] == ',')
						tempString = tempString.Substring (0, tempString.Length - 1);

					highScores.Add (JsonUtility.FromJson<TopTenScore> (tempString));
					print ("PlayerName: " + highScores [i].PlayerName + " Score: " + highScores [i].Score);
					//scoreListings [i].text = gameTypes [i].PlayerName + ": " + gameTypes [i].Score;
					i++;
				}
			}
		}
		yield return null;
	}
}