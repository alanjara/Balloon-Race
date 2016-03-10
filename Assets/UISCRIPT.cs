using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UISCRIPT : MonoBehaviour {
    public Text timertext, leadertext;
    
	// Use this for initialization
	void Start () {
	}
	
    void FixedUpdate()
    {
        PersistentGameData.raceTimer += Time.deltaTime;
        timertext.text = PersistentGameData.raceTimer.ToString("F2");
    }

	// Update is called once per frame
	void Update () {
	    
	}
}
