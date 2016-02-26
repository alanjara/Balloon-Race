using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour {
    public GameObject target;
    public float FollowDistance = 7.5f;
    public float speed = 2f;
    public float distThres = .01f;
    public Vector3 newPos;
    public float MAXy;
    public static cameraFollow S;
    Vector3 cam_target_position;

    public void GameOver() {
        Application.LoadLevel(Application.loadedLevel);
    }


    void Awake() {
        S = this;
    }
    void Start() {
        newPos = Camera.main.transform.position;
        newPos.y = target.transform.position.y + 4f;
        newPos.z = target.transform.position.z - FollowDistance;
        Camera.main.transform.position = newPos;
    }
    void FixedUpdate() {

        newPos = Camera.main.transform.position;
        newPos.x = target.transform.position.x;
        newPos.y = target.transform.position.y + 4f;
        newPos.z = target.transform.position.z - FollowDistance;
        if (newPos.y > MAXy - 1) {
            newPos.y = MAXy - 1;
        }


        cam_target_position = newPos;
        Vector3 to_player = transform.position - cam_target_position;
      //  to_player.z = 0;
        if (to_player.magnitude > 1) {
            Vector3 new_pos = transform.position;
            Vector3 displacement = cam_target_position - transform.position;
            float multiplier = 1;
            //displacement.z = 0;
            if (displacement.magnitude > 4)
                multiplier = displacement.magnitude / 4;
            displacement.Normalize();
            displacement = displacement * 5f * multiplier * multiplier * Time.deltaTime;
            while (to_player.magnitude < displacement.magnitude)
                displacement = displacement * 0.9f;
            new_pos += displacement;
            transform.position = new_pos;
        }

/*
            if (Mathf.Abs(newPos.z - Camera.main.transform.position.z) > distThres)
            {
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, newPos, Time.deltaTime * speed + Mathf.Abs(target.GetComponent<Rigidbody>().velocity.z) / 30f);
            }
            else
            {
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, newPos, Time.deltaTime * speed);
            }
        */
        Quaternion lookrot = Quaternion.LookRotation(target.transform.position - transform.position);

        // Smoothly rotate towards the target point.
        transform.rotation = Quaternion.Slerp(transform.rotation, lookrot, speed * Time.deltaTime);
        // transform.LookAt (target.transform);
    }

}