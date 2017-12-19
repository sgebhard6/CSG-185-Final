using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PostGameType : MonoBehaviour
{
	public delegate void GameTypeDelegate (int gameTypeID);

	public static event GameTypeDelegate OnPost;

	public string gameTypeName;
	public string targetURL;

	public int GameTypeID{ get; private set; }

	public List<GameType> gameTypes;

	bool gameTypeFound;

	void Awake ()
	{
		StartCoroutine (GetGameTypes ());
		//StartCoroutine (Post ());
	}

	void Start ()
	{
		//StartCoroutine (Post ());
	}

	IEnumerator GetGameTypes ()
	{
		gameTypes.Clear ();
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

					gameTypes.Add (JsonUtility.FromJson<GameType> (tempString));
					//scoreListings [i].text = gameTypes [i].PlayerName + ": " + gameTypes [i].Score;
					i++;
				}
			}

			StartCoroutine (Post ());
		}
		yield return null;
	}

	public int GetGameTypeID ()
	{
		int gameTypeID = 0;
		for (int i = 0; i < gameTypes.Count; i++) {
			if (gameTypes [i].GameTypeName == gameTypeName)
				gameTypeID = gameTypes [i].GameTypeID;
		}

		return gameTypeID;
	}

	IEnumerator Post ()
	{
		for (int i = 0; i < gameTypes.Count; i++) {
			if (gameTypes [i].GameTypeName == gameTypeName)
				gameTypeFound = true;
		}

		if (!gameTypeFound) {
			GameType gameTypeToPost = new GameType (gameTypeName);
			UnityWebRequest www = UnityWebRequest.Post ("http://localhost:83/api/GameTypes", "");
			UploadHandler customUploadHandler = new UploadHandlerRaw (System.Text.Encoding.UTF8.GetBytes (JsonUtility.ToJson (gameTypeToPost)));
			customUploadHandler.contentType = "application/json";
			www.uploadHandler = customUploadHandler;
			yield return www.Send ();

			if (www.error != null)
				Debug.Log ("Error: " + www.error);
			else
				Debug.Log ("Success: " + www.responseCode);
		}
	}
}