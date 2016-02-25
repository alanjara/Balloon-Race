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
		SceneManager.LoadScene ("_1PlyrBloonSel");
	}

	void loadTwoPlayer(){
		SceneManager.LoadScene ("_1PlyrBloonSel");
	}
}
