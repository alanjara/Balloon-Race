using UnityEngine;
using System.Collections;

public class windGust : MonoBehaviour {
    public float density = 1f;
    public float strength = 1;
    float height, width;
    LayerMask balloon_parts;
    Vector3 topleft;
    // Use this for initialization
    void Start() {
        balloon_parts = LayerMask.GetMask("Balloon", "Basket");

    }

    // Update is called once per frame
    void FixedUpdate() {
        height = transform.localScale.y;
        width = transform.localScale.x;
        topleft = transform.position + transform.up * transform.localScale.y / 2 - transform.right * transform.localScale.x / 2;
        float xdensity = width * density;
        float ydensity = height * density;
        topleft = topleft - transform.up * height / ydensity / 2 + transform.right * width / xdensity / 2;
        for (int x = 0; x < xdensity; x++) {
            for (int y = 0; y < ydensity; y++) {
                Vector3 gust_origin = topleft - transform.up * y / ydensity * height + transform.right * x / xdensity * width;
                Vector3 gust = gust_origin + transform.forward *50;
                RaycastHit hit;
                if (Physics.Raycast(gust_origin, gust, out hit, 100, balloon_parts)) {
                    hit.transform.GetComponent<Rigidbody>().AddForceAtPosition(gust * strength/100000, hit.point,ForceMode.Acceleration);
                }
                    Debug.DrawLine(gust_origin, gust);

            }
        }
    }
}
