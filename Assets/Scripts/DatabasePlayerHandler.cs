using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DatabasePlayerHandler : MonoBehaviour
{
	public delegate void PlayerDelegate (int playerID);

	public static event PlayerDelegate OnPost;

	public string playerName;

	public int PlayerID { get; private set; }

	void Start ()
	{
		StartCoroutine (Post ());
	}

	IEnumerator Post ()
	{
		Player playerToPost = new Player (playerName);
		UnityWebRequest www = UnityWebRequest.Post ("http://localhost:83/api/Players", "");
		UploadHandler customUploadHandler = new UploadHandlerRaw (System.Text.Encoding.UTF8.GetBytes (JsonUtility.ToJson (playerToPost)));
		customUploadHandler.contentType = "application/json";
		www.uploadHandler = customUploadHandler;
		yield return www.Send ();

		string playerIDString = www.GetResponseHeader ("Location").Substring (32);
		PlayerID = System.Convert.ToInt32 (playerIDString);

		if (www.error != null)
			Debug.Log ("Error: " + www.error);
		else
			Debug.Log ("Success: " + www.responseCode);

		OnPost (PlayerID);
	}
}