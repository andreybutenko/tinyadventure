using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpCooldown = 1.0f;
    public float jumpBoostY = 0.2f;
    public float jumpForceY = 500;
    protected float prevJump = 0;    
    protected new Rigidbody rigidbody;

    public void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update() {
        if((Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space)) && prevJump < Time.time - jumpCooldown) {
            Jump();
        }

        if(Input.GetMouseButton(0)) {
            RaycastHit hit;

            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000)) {
                Vector3 lookAtPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                gameObject.transform.LookAt(lookAtPos);

                Vector3 velocity = transform.forward * speed;
                velocity.y = rigidbody.velocity.y;
                rigidbody.velocity = velocity;
            }
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) {
            float dx = Input.GetAxis("Horizontal");
            float dz = Input.GetAxis("Vertical");

            // rotate 55deg because of angle of iso camera
            Vector3 lookAtPos = transform.position + Quaternion.AngleAxis(-55f, Vector3.up) * (dz * Vector3.forward + dx * Vector3.right);
            gameObject.transform.LookAt(lookAtPos);

            Vector3 velocity = transform.forward * speed;
            velocity.y = rigidbody.velocity.y;
            rigidbody.velocity = velocity;
        }
        else {
            StopMovement();
        }
    }

    public void Jump() {
        // Jump boost or else player will get stuck on the edges of  objects
        transform.Translate(0, jumpBoostY, 0);
        rigidbody.AddForce(0, jumpForceY, 0, ForceMode.Impulse);
        prevJump = Time.time;
    }

    public void StopMovement() {
        rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
    }
}
