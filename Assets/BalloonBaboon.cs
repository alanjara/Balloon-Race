using UnityEngine;
using System.Collections;

public class BalloonBaboon : MonoBehaviour {
    Animator anm;
	// Use this for initialization
	void Start () {
        anm = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void Crouch()
    {
        anm.SetTrigger("Crouch");
    }

    public void Damaged()
    {
        anm.SetTrigger("Damaged");
    }

    public void Celebration()
    {
        anm.SetTrigger("Celebration");
    }

    public void UsedItem()
    {
        anm.SetTrigger("UsedItem");
    }

    public void Boost()
    {
        anm.SetTrigger("Boost");
    }

    public void CrouchShort()
    {
        anm.SetTrigger("CrouchShort");
    }
}
