using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMenuUI : MonoBehaviour {

	public Button onePlayer;
	public Button twoPlayers;

	void Start(){
		onePlayer.onClick.AddListener(() => loadOnePlayer());
		twoPlayers.onClick.AddListener(() => loadTwoPlayer());
	}

	void loadOnePlayer(){
		SceneManager.LoadScene ("SceneAlan");
	}

	void loadTwoPlayer(){
		SceneManager.LoadScene ("SceneAlan");
	}
}
