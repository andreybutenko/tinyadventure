using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavFollowController : PlayerController
{
    public GameObject master;
    public float targetDistance = 2.5f;
    public float moveDistance = 5;
    public float teleportDistance = 25;
    public float jumpForwardForce = 5f;
    bool hasTarget = false;
    bool jumping = true;
    NavMeshAgent agent;

    new void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
    }

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
        else if(Vector3.Distance(transform.position, master.transform.position) > targetDistance && hasTarget && !jumping) {
            hasTarget = true;
            agent.SetDestination(master.transform.position);
        }
        else if(Vector3.Distance(transform.position, master.transform.position) > moveDistance && !jumping) {
            hasTarget = true;
            agent.SetDestination(master.transform.position);
        }
        else if(!jumping) {
            hasTarget = false;
            agent.SetDestination(transform.position);
            StopMovement();
        }

        // Jump if there's an obstacle ahead
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 0.5f) && prevJump < Time.time - jumpCooldown) {
            agent.enabled = false;
            prevJump = Time.time;
            Jump();
            jumping = true;
        }

        // If jumping, apply force forward to overcome obstacle
        if(jumping) {
            rigidbody.AddForce(jumpForwardForce * transform.forward * Time.deltaTime, ForceMode.Impulse);
        }

        if(jumping && prevJump < Time.time - jumpCooldown) {
            agent.enabled = true;
            jumping = false;
        }
    }
}
