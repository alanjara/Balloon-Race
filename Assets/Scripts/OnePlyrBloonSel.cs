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

	public GameObject balloon;
	public GameObject basket;

	public List<Color> colorlist;
	public int colorCount;

	public List<Mesh> meshlist;
	public int meshCount;
	// Use this for initialization
	void Start () {
		balloon = GameObject.Find ("HotAirBalloon/balloon");
		basket = GameObject.Find ("HotAirBalloon/basket");
		GoButton.onClick.AddListener(() => loadOnePlayer());
		BalloonRightButton.onClick.AddListener (() => incrementColor ());
		BalloonLeftButton.onClick.AddListener (() => decrementColor ());
		BasketRightButton.onClick.AddListener (() => incrementBalloon ());
		BasketLeftButton.onClick.AddListener (() => decrementBalloon ());

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

		meshCount = 0;
	}
	
	void loadOnePlayer(){
		SceneManager.LoadScene ("SceneAlan");
	}

	void incrementColor(){
		colorCount++;
		if (colorCount >= colorlist.Count)
			colorCount = 0;
		balloon.GetComponent<Renderer> ().material.color = colorlist[colorCount];
	}

	void decrementColor(){
		colorCount--;
		if (colorCount < 0)
			colorCount = colorlist.Count - 1;
		balloon.GetComponent<Renderer> ().material.color = colorlist[colorCount];
	}

	void incrementBalloon(){
		meshCount++;
		if (meshCount >= meshlist.Count)
			meshCount = 0;
		balloon.GetComponent<MeshFilter> ().mesh = meshlist [meshCount];
	}

	void decrementBalloon(){
		meshCount--;
		if (meshCount < 0)
			meshCount = meshlist.Count - 1;
		balloon.GetComponent<MeshFilter> ().mesh = meshlist [meshCount];
	}
}
