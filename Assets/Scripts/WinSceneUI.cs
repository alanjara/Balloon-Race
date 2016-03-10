using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class WinSceneUI : MonoBehaviour {

	public Button MainMenu;
	public float speed;

	void Start () {
		MainMenu.onClick.AddListener(() => loadMainMenu());
		GameObject.Find("Player_").GetComponent<Text>().text += PersistentGameData.raceWinner;
	}

	void Update(){
		Color color = GameObject.Find ("Player_").GetComponent<Text> ().color;
		color.g = (Mathf.Sin (Time.time * speed) + 1.0f) / 2.0f;
		color.b = (Mathf.Sin (Time.time * speed) + 1.0f) / 2.0f;
		GameObject.Find("Player_").GetComponent<Text>().color = color;
	}

	void loadMainMenu(){
		SceneManager.LoadScene ("_MainMenu");
	}
}
