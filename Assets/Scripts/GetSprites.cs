using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GetSprites : MonoBehaviour
{
	public delegate void SpriteDelegate ();

	public static event SpriteDelegate OnGetSprites;

	public List<Sprite> spriteList;
	public string imageFolderName;

	string imageFile;
	string[] imageFilePaths;

	void Awake ()
	{
		imageFile = Path.Combine (Application.streamingAssetsPath, imageFolderName);
		imageFilePaths = Directory.GetFiles (imageFile);
		StartCoroutine (FindSprites (imageFilePaths, spriteList));
	}

	IEnumerator FindSprites (string[] filePaths, List<Sprite> spriteList)
	{
		Texture2D temp = new Texture2D (10, 10);
		for (int i = 0; i < filePaths.Length; i++) {
			if (filePaths [i].Substring (filePaths [i].Length - 5) != ".meta") {
				WWW www = new WWW ("file://" + Path.GetFullPath (filePaths [i]));
				yield return www;
				if (!string.IsNullOrEmpty (www.error))
					print (www.error);
				temp = www.texture;
				Sprite sprite = Sprite.Create (temp, new Rect (0, 0, temp.width, temp.height), new Vector2 (0.5f, 0.5f));
				spriteList.Add (sprite);
			}
		}

		OnGetSprites ();
	}
}
