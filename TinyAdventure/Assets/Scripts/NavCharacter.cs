using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavCharacter : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject child;
    Rigidbody childRigidbody;

    public int jumpCooldown = 3;
    public float jumpThreshold = 0.1f;
    float lastJump = 0;
    float prevY;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        child = transform.GetChild(0).gameObject;
        childRigidbody = child.GetComponent<Rigidbody>();
        prevY = transform.position.y;
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            RaycastHit hit;

            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000)) {
                agent.destination = hit.point;
            }
        }

        if(transform.position.y > prevY + jumpThreshold && lastJump < Time.time - jumpCooldown) {
            childRigidbody.AddForce(0, 200, 0);
            lastJump = Time.time;
            // agent.velocity = rigidbody.velocity;
            // agent.enabled = false;
        }

        prevY = transform.position.y;

        child.transform.position = new Vector3(
            transform.position.x,
            child.transform.position.y,
            transform.position.z
        );
    }
}
