using UnityEngine;
using System.Collections;

public class Basket : MonoBehaviour {

    public Component[] springJoints;
    public Transform[] anchors;
    public Transform[] balloon_anchors;
    public LineRenderer[] ropes;
    float ropemod = 0.5f;
    float top_offset = 0;
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

        if (balloon.GetComponent<balloon_base>().my_number == 1) {
            balloon.GetComponent<Renderer>().material.color = PersistentGameData.player1balloonColor;
            if (PersistentGameData.meshnum1 != 0) {
                balloon.GetComponent<MeshFilter>().mesh = PersistentGameData.player1balloonModel;
                if (PersistentGameData.meshnum1 == 1) {
                   top_offset =  -(0.9f-.116f);
                }
                if (PersistentGameData.meshnum1 == 2) {
                    top_offset = - (0.9f - 1.22f);
                }
                if (PersistentGameData.meshnum1 == 3) {
                    top_offset = -(0.9f + .62f);

                }
                if (PersistentGameData.meshnum1 == 4) {
                    ropemod = 1f;
                    top_offset =  -(.9f - .771f);
                }
                foreach (Transform x in balloon_anchors) {
                    x.position += Vector3.up *ropemod;
                }
                transform.parent.transform.Find("balloon").transform.Find("RallyingBaboon (1)").transform.position += Vector3.up * top_offset;
            }
        } else if (balloon.GetComponent<balloon_base>().my_number == 2) {
            balloon.GetComponent<Renderer>().material.color = PersistentGameData.player2balloonColor;
            if (PersistentGameData.meshnum2 !=0) {
                balloon.GetComponent<MeshFilter>().mesh = PersistentGameData.player2balloonModel;
                if (PersistentGameData.meshnum2 == 1) {
                    top_offset = -(0.9f - .116f);
                }
                if (PersistentGameData.meshnum2 == 2) {
                    top_offset = -(0.9f - 1.22f);
                }
                if (PersistentGameData.meshnum2 == 3) {
                    top_offset = -(0.9f + .62f);

                }
                if (PersistentGameData.meshnum2 == 4) {
                    ropemod = 1f;
                    top_offset = -(.9f - .771f);
                }
                foreach (Transform x in balloon_anchors) {
                    x.position += Vector3.up * ropemod;
                }
                transform.parent.transform.Find("balloon").transform.Find("RallyingBaboon (1)").transform.position += Vector3.up * top_offset;
            }
        } else if (balloon.GetComponent<balloon_base>().my_number == 3)
        {
            foreach (Transform x in balloon_anchors) {
                    x.position += Vector3.up;
                }
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
