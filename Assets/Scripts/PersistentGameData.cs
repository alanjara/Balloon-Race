using UnityEngine;
using System.Collections;

public class PersistentGameData {

	static public int numPlayers = 1;//1 for 1player, 2 for 2player
	static public int player1balloonModel = 0;//0=normal, 1=blimp, 2=icecream, 3=banana, 4=monkey 
	static public int player2balloonModel = 0;
	static public int player1balloonColor = 0;//0=red, 1=blue, 2=green, 3=yellow, 4=magenta, 5=cyan, 6=black
	static public int player2balloonColor = 0;
	static public int raceWinner = 0;//use this for a race win scene?
}
