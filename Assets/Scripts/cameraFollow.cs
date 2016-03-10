using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour {
    public GameObject target;
    public float FollowDistance = 7.5f;
    public float speed = 2f;
    public float distThres = .01f;
    public Vector3 targetPos;
    public float MAXy;
    Vector3 new_pos;
    public static cameraFollow S;
    Vector3 cam_target_position;
    Vector3 start_pos;
    Quaternion start_rot;
    float start_time;
    Quaternion lookrot;

    public void GameOver() {
        Application.LoadLevel(Application.loadedLevel);
    }


    void Awake() {
        S = this;
    }
    void Start() {
        targetPos = Camera.main.transform.position;
        targetPos.y = target.transform.position.y + 4f;
        targetPos.z = target.transform.position.z - FollowDistance;
        Camera.main.transform.position = targetPos;
        start_time = Time.time;
      
    }
    void FixedUpdate() {
        start_time = Time.time;
        targetPos = Camera.main.transform.position;
        targetPos = target.transform.position;
    //    targetPos.x = target.transform.position.x;
     //   targetPos.y = target.transform.position.y + 4f;
      //  targetPos.z = target.transform.position.z - FollowDistance;
        targetPos = targetPos - target.GetComponent<balloon_base>().race_forward * FollowDistance + 4f * target.GetComponent<balloon_base>().race_up;
        if (targetPos.y > MAXy - 1) {
            targetPos.y = MAXy - 1;
        }


        cam_target_position = targetPos;
        Vector3 to_player = transform.position - cam_target_position;
        start_pos = transform.position;
      //  to_player.z = 0;
        
        if (to_player.magnitude > 0) {
             new_pos = transform.position;
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
            targetPos = new_pos;
            transform.position = new_pos;
        } 
         

        start_rot = transform.rotation;
         lookrot = Quaternion.LookRotation(target.transform.position - transform.position);
       //  if (Quaternion.Angle(start_rot, lookrot) < 1)
     //        lookrot = start_rot;
        // Smoothly rotate towards the target point.
        transform.rotation = Quaternion.Slerp(transform.rotation, lookrot, speed * Time.deltaTime);
        // transform.LookAt (target.transform);
    }

}