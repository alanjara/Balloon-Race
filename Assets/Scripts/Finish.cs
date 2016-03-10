using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour
{
    bool over = false;
    // Use this for initialization
    void Start()
    {

    }
    public GameObject levelendtext;

    IEnumerator levelended()
    {
        levelendtext.SetActive(true);
        yield return new WaitForSeconds(3f);
        Application.LoadLevel("WinScene");

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (over)
        {
            return;
        }
        if(collision.gameObject.tag == "Player")
        {
            over = true;
            // winrar!
            PersistentGameData.raceWinner = collision.gameObject.GetComponent<balloon_base>().my_number;
            StartCoroutine(levelended());

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
