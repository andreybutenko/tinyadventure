using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : PlayerController
{
    public GameObject master;
    public float maxDistance = 5;
    public float teleportDistance = 150;

    // Update is called once per frame
    void Update()
    {
        Vector3 lookAtPos = new Vector3(
            master.transform.position.x,
            transform.position.y,
            master.transform.position.z
        );
        transform.LookAt(lookAtPos);

        if(Vector3.Distance(transform.position, master.transform.position) > teleportDistance) {
            transform.position = master.transform.position + Vector3.up * 5;
        }
        else if(Vector3.Distance(transform.position, master.transform.position) > maxDistance) {
            Vector3 velocity = transform.forward * speed;
            velocity.y = rigidbody.velocity.y;
            rigidbody.velocity = velocity;
        }
        else {
            StopMovement();
        }

        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 0.5f) && prevJump < Time.time - jumpCooldown) {
            prevJump = Time.time;
            Jump();
        }
    }
}
