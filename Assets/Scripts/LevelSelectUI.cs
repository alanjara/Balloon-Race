using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;	

public class LevelSelectUI : MonoBehaviour {
	public Transform baboonPosition;
	public Button Level1;
	public Button Level2;
	public Button Level3;

	// Use this for initialization
	void Start () {
		Level1.onClick.AddListener(() => loadLevel1());
		Level2.onClick.AddListener(() => loadLevel2());
		Level3.onClick.AddListener(() => loadLevel3());
	}
	
	// Update is called once per frame
	void Update () {
		if (baboonPosition.position.x > .5)
			Level3.Select();
		if (baboonPosition.position.x <= .5 && baboonPosition.position.x >= -.5)
			Level2.Select();
		if (baboonPosition.position.x < -.5)
			Level1.Select();
	}
		
	void loadLevel1(){
		SceneManager.LoadScene ("Scene_Stali");
	}
	void loadLevel2(){
		SceneManager.LoadScene ("testVol");
	}
	void loadLevel3(){
		SceneManager.LoadScene ("CityComplete");
	}
}
