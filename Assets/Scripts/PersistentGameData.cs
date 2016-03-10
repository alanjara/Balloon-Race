using UnityEngine;
using System.Collections;

public class PersistentGameData {

	static public int numPlayers = 1;//1 for 1player, 2 for 2player
	static public int AIdifficulty = 0;//0=easy, 1=medium, 2=hard, 3=absolutelyBANANAS
    static public Mesh player1balloonModel;// = 0;//0=normal, 1=blimp, 2=icecream, 3=banana, 4=monkey 
	static public Mesh player2balloonModel;// = 0;
    static public int meshnum1=0;
    static public int meshnum2=0;
	static public Color player1balloonColor = Color.red;//0=red, 1=blue, 2=green, 3=yellow, 4=magenta, 5=cyan, 6=black
	static public Color player2balloonColor = Color.red;
	static public int raceWinner = 0;//use this for a race win scene?
    static public float raceTimer = 0f;
    static public float raceEndTime = 0f;
}
