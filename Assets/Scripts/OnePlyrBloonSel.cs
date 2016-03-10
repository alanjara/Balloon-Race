using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class OnePlyrBloonSel : MonoBehaviour {
	public Button GoButton;
	public Button BalloonRightButton;
	public Button BalloonLeftButton;
	public Button BasketRightButton;
	public Button BasketLeftButton;
	public Button DiffLeftButton;
	public Button DiffRightButton;

	public GameObject balloon;
	public GameObject basket;

	public List<Color> colorlist;
	public int colorCount;

	public List<Mesh> meshlist;
	public int meshCount;

	public List<string> diffList;
	public int diffCount;
	public Text Difficulty;
	// Use this for initialization
	void Start () {
		PersistentGameData.numPlayers = 1;

		balloon = GameObject.Find ("HotAirBalloon/balloon");
		basket = GameObject.Find ("HotAirBalloon/basket");
		GoButton.onClick.AddListener(() => loadOnePlayer());
		BalloonRightButton.onClick.AddListener (() => incrementColor ());
		BalloonLeftButton.onClick.AddListener (() => decrementColor ());
		BasketRightButton.onClick.AddListener (() => incrementBalloon ());
		BasketLeftButton.onClick.AddListener (() => decrementBalloon ());
		DiffRightButton.onClick.AddListener (() => incrementDifficulty ());
		DiffLeftButton.onClick.AddListener (() => decrementDifficulty ());

		colorCount = 0;
		colorlist = new List<Color>(){
			Color.red,
			Color.blue,
			Color.green,
			Color.yellow,
			Color.magenta,
			Color.cyan,
			Color.black,
		};
			
		diffCount = 0;
		diffList = new List<string>(){
			"Easy",
			"Medium",
			"Hard",
			"Absolutely\nBananas"
		};
		Difficulty.GetComponent<Text>().text = diffList[diffCount];

		meshCount = 0;
	}
	
	void loadOnePlayer(){
		SceneManager.LoadScene ("LevelSelect");
	}

	void incrementColor(){
		colorCount++;
		if (colorCount >= colorlist.Count)
			colorCount = 0;
		balloon.GetComponent<Renderer> ().material.color = colorlist[colorCount];
		PersistentGameData.player1balloonColor = colorCount;
	}

	void decrementColor(){
		colorCount--;
		if (colorCount < 0)
			colorCount = colorlist.Count - 1;
		balloon.GetComponent<Renderer> ().material.color = colorlist[colorCount];
		PersistentGameData.player1balloonColor = colorCount;
	}

	void incrementBalloon(){
		meshCount++;
		if (meshCount >= meshlist.Count)
			meshCount = 0;
		balloon.GetComponent<MeshFilter> ().mesh = meshlist [meshCount];
		PersistentGameData.player1balloonModel = meshCount;
	}

	void decrementBalloon(){
		meshCount--;
		if (meshCount < 0)
			meshCount = meshlist.Count - 1;
		balloon.GetComponent<MeshFilter> ().mesh = meshlist [meshCount];
		PersistentGameData.player1balloonModel = meshCount;
	}

	void incrementDifficulty(){
		diffCount++;
		if (diffCount >= diffList.Count)
			diffCount = 0;
		Difficulty.GetComponent<Text>().text = diffList[diffCount];
	}

	void decrementDifficulty(){
		diffCount--;
		if (diffCount < 0)
			diffCount = diffList.Count - 1;
		Difficulty.GetComponent<Text>().text = diffList[diffCount];
	}
}
