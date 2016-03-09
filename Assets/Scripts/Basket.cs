using UnityEngine;
using System.Collections;

public class Basket : MonoBehaviour {

    public Component[] springJoints;
    public Transform[] anchors;
    public Transform[] balloon_anchors;
    public LineRenderer[] ropes;
    void Start() {
        int index = 0;
        ropes = new LineRenderer[4];
        GameObject balloon = transform.parent.transform.Find("balloon").gameObject;
        springJoints = GetComponents<SpringJoint>();
        anchors = new Transform[4];
        balloon_anchors = new Transform[4];
        foreach (SpringJoint joint in springJoints) {
            ropes[index] = Instantiate<LineRenderer>(transform.parent.Find("rope").GetComponent<LineRenderer>());
            string anchor = string.Format("anchor {0}", index);
            anchors[index] = gameObject.transform.Find(anchor).transform;
            balloon_anchors[index] = balloon.transform.Find(anchor).transform;
            joint.anchor = anchors[index].localPosition;
            joint.connectedBody = balloon.GetComponent<Rigidbody>();
            // joint.anchor = balloon.transform.Find(anchor).transform.localPosition;
            joint.connectedAnchor = balloon_anchors[index].localPosition;
          //  joint.connectedAnchor = balloon.transform.localPosition - balloon.transform.up * 1;

            joint.spring = 5;
            joint.maxDistance = .5f;
            index++;

        }
    }
    // Use this for initialization

    // Update is called once per frame
    void Update() {
        for (int i = 0; i < 4; i++) {
            ropes[i].SetPosition(0, anchors[i].position);
            ropes[i].SetPosition(1, balloon_anchors[i].position);
        }


    }
}
