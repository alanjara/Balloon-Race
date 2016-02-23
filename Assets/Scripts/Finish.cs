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
        yield return new WaitForSeconds(1.4f);
        Application.LoadLevel(Application.loadedLevel);

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

            StartCoroutine(levelended());

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
