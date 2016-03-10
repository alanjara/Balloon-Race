using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TwoPlyrBloonSel : MonoBehaviour {
    public Button GoButton;
    public Button BalloonRightButton1;
    public Button BalloonLeftButton1;
    public Button BasketRightButton1;
    public Button BasketLeftButton1;
    public Button BalloonRightButton2;
    public Button BalloonLeftButton2;
    public Button BasketRightButton2;
    public Button BasketLeftButton2;

    public GameObject balloon1;
    public GameObject basket1;
    public GameObject balloon2;
    public GameObject basket2;

    public List<Color> colorlist;
    public int colorCount;

    public List<Mesh> meshlist;
    public int meshCount;
    // Use this for initialization
    void Start() {
        PersistentGameData.numPlayers = 2;

        balloon1 = GameObject.Find("HotAirBalloon1/balloon");
        basket1 = GameObject.Find("HotAirBalloon1/basket");
        balloon2 = GameObject.Find("HotAirBalloon2/balloon");
        basket2 = GameObject.Find("HotAirBalloon2/basket");

        GoButton.onClick.AddListener(() => loadOnePlayer());
        BalloonRightButton1.onClick.AddListener(() => incrementColor(1));
        BalloonLeftButton1.onClick.AddListener(() => decrementColor(1));
        BasketRightButton1.onClick.AddListener(() => incrementBalloon(1));
        BasketLeftButton1.onClick.AddListener(() => decrementBalloon(1));
        BalloonRightButton2.onClick.AddListener(() => incrementColor(2));
        BalloonLeftButton2.onClick.AddListener(() => decrementColor(2));
        BasketRightButton2.onClick.AddListener(() => incrementBalloon(2));
        BasketLeftButton2.onClick.AddListener(() => decrementBalloon(2));

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
        balloon1.GetComponent<MeshFilter>().mesh = meshlist[meshCount];
        PersistentGameData.player1balloonModel = meshlist[meshCount];
        PersistentGameData.meshnum1 = meshCount;
        balloon2.GetComponent<MeshFilter>().mesh = meshlist[meshCount];
        PersistentGameData.player2balloonModel = meshlist[meshCount];
        PersistentGameData.meshnum2 = meshCount;
        balloon1.GetComponent<Renderer>().material.color = colorlist[colorCount];
        PersistentGameData.player1balloonColor = colorlist[colorCount];

        balloon2.GetComponent<Renderer>().material.color = colorlist[colorCount];
        PersistentGameData.player2balloonColor = colorlist[colorCount];

    }

    void loadOnePlayer() {
        SceneManager.LoadScene("LevelSelect");
    }

    void incrementColor(int playernum) {
        colorCount++;
        if (colorCount >= colorlist.Count)
            colorCount = 0;
        if (playernum == 1) {
            balloon1.GetComponent<Renderer>().material.color = colorlist[colorCount];
            PersistentGameData.player1balloonColor = colorlist[colorCount];
        } else {
            balloon2.GetComponent<Renderer>().material.color = colorlist[colorCount];
            PersistentGameData.player2balloonColor = colorlist[colorCount];
        }
    }

    void decrementColor(int playernum) {
        colorCount--;
        if (colorCount < 0)
            colorCount = colorlist.Count - 1;
        if (playernum == 1) {
            balloon1.GetComponent<Renderer>().material.color = colorlist[colorCount];
            PersistentGameData.player1balloonColor = colorlist[colorCount];
        } else {
            balloon2.GetComponent<Renderer>().material.color = colorlist[colorCount];
            PersistentGameData.player2balloonColor = colorlist[colorCount];
        }
    }

    void incrementBalloon(int playernum) {
        meshCount++;
        if (meshCount >= meshlist.Count)
            meshCount = 0;
        if (playernum == 1) {
            balloon1.GetComponent<MeshFilter>().mesh = meshlist[meshCount];
            PersistentGameData.player1balloonModel = meshlist[meshCount];
            PersistentGameData.meshnum1 = meshCount;
        } else {
            balloon2.GetComponent<MeshFilter>().mesh = meshlist[meshCount];
            PersistentGameData.player2balloonModel = meshlist[meshCount];
            PersistentGameData.meshnum2 = meshCount;
        }
    }

    void decrementBalloon(int playernum) {
        meshCount--;
        if (meshCount < 0)
            meshCount = meshlist.Count - 1;
        if (playernum == 1) {
            balloon1.GetComponent<MeshFilter>().mesh = meshlist[meshCount];
            PersistentGameData.player1balloonModel = meshlist[meshCount];
            PersistentGameData.meshnum1 = meshCount;
        } else {
            balloon2.GetComponent<MeshFilter>().mesh = meshlist[meshCount];
            PersistentGameData.player2balloonModel = meshlist[meshCount];
            PersistentGameData.meshnum2 = meshCount;
        }
    }
}
