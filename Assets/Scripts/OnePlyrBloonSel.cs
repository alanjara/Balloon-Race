using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class OnePlyrBloonSel : MonoBehaviour {
	public Button GoButton;
	public Button BalloonRightButton;
	public Button BalloonLeftButton;
	public Button BasketRightButton;
	public Button BasketLeftButton;

	public GameObject balloon;
	// Use this for initialization
	void Start () {
		GoButton.onClick.AddListener(() => loadOnePlayer());
	}
	
	void loadOnePlayer(){
		SceneManager.LoadScene ("SceneAlan");
	}

	void incrementColor(){

	}
}
