using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class aimingControl : MonoBehaviour {

    public GameObject crosshair;
    public GameObject target;
    public Camera camera;
    public balloon_base balloon;
    public bool ifAim = true;
    public bool angleSmall = true;
	// Use this for initialization
	void Start () {
        
       
	}
	
	// Update is called once per frame
	void Update () {
        float angle = Vector3.Angle(balloon.race_forward, target.transform.position - balloon.transform.position);
        if (angle > 45 || angle < -45)
        {
            angleSmall = false;
        }
        else
        {
            angleSmall = true;
        }
        if (ifAim && angleSmall)
        {
            crosshair.GetComponent<Image>().enabled = true;
            Vector3 pos = camera.WorldToScreenPoint(target.transform.position);
            crosshair.transform.position = pos;
        }
        else
        {
            crosshair.GetComponent<Image>().enabled = false;
        }
    }
    public void setIfAim(bool TF)
    {
        ifAim = TF;
    }
}
