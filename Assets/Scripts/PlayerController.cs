using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // walking speed
    public float walkSpeed;

    // jumping force
    public float jumpForce;

    //coin playing sound
    public AudioSource coinSound;

    // camera distance z
    public float cameraDistZ = 6;

    // Rigidbody component
    Rigidbody rb;

    // Collider
    Collider col;

    // flag to keep track of pressing
    bool pressedJump = false;

    // size of player
    Vector3 size;

    // y that represent that you fell
    float minY = -.7f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        // get player size
        size = col.bounds.size;

        // set the camera position
        CameraFollowPlayer();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        WalkHandler();
        JumpHandler();
        CameraFollowPlayer();
        FallHandler();
    }

    // check if player fell
    void FallHandler() {
        if(transform.position.y <= minY) {
            // Game over!
            GameManager.instance.GameOver();
        }
    }

    void WalkHandler() {
        // Input on X
        float hAxis = Input.GetAxis("Horizontal");

        // Input on Y
        float vAxis = Input.GetAxis("Vertical");

        // Movement vector
        Vector3 movement = new Vector3(hAxis * walkSpeed * Time.deltaTime, 0, vAxis * walkSpeed * Time.deltaTime);

        // Calculate the new postion
        Vector3 newPos = transform.position + movement;

        // Move
        rb.MovePosition(newPos);

        // Check that we are moving
        if(hAxis != 0 || vAxis != 0) {
            // Movement direction
            Vector3 direction = new Vector3(hAxis, 0, vAxis);

            rb.rotation = Quaternion.LookRotation(direction);
        }
    }

    void JumpHandler() {
        // Input on the Jump Axis
        float jAxis = Input.GetAxis("Jump");

        // If the key has been pressed
        if(jAxis > 0) {
            bool isGrounded = CheckGrounded();

            // make sure we aren't already jumping
            if (!pressedJump && isGrounded) {
                pressedJump = true;

                // jumping vector
                Vector3 jumpVector = new Vector3(0, jAxis * jumpForce, 0);

                // Apply force
                rb.AddForce(jumpVector, ForceMode.VelocityChange);
            }
        } else {
            // set flag to false
            pressedJump = false;
        }
    }

    bool CheckGrounded() {
        // location of all 4 corners
        Vector3 corner1 = transform.position + new Vector3(size.x/2, -size.y/2 + 0.01f, size.z/2);
        Vector3 corner2 = transform.position + new Vector3(-size.x/2, -size.y/2 + 0.01f, size.z/2);
        Vector3 corner3 = transform.position + new Vector3(size.x/2, -size.y/2 + 0.01f, -size.z/2);
        Vector3 corner4 = transform.position + new Vector3(-size.x/2, -size.y/2 + 0.01f, -size.z/2);

        // check if we are grounded
        bool grounded1 = Physics.Raycast(corner1, -Vector3.up, 0.02f);
        bool grounded2 = Physics.Raycast(corner2, -Vector3.up, 0.02f);
        bool grounded3 = Physics.Raycast(corner3, -Vector3.up, 0.02f);
        bool grounded4 = Physics.Raycast(corner4, -Vector3.up, 0.02f);

        return(grounded1||grounded2||grounded3||grounded4);
    }

    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Coin")) {
            // Increase our score
            GameManager.instance.IncreaseScore(1);

            //Play sound
            coinSound.Play();

            Destroy(other.gameObject);
        } else if(other.CompareTag("Enemy")) {
            // game over
            GameManager.instance.GameOver();
        } else if(other.CompareTag("Goal")) {
            // send user to next level
            GameManager.instance.IncreaseLevel();
        }
    }

    void CameraFollowPlayer() {
        // grab the camera position
        Vector3 cameraPos = Camera.main.transform.position;

        // modify it's position according to cameraDistz
        cameraPos.z = transform.position.z - cameraDistZ;

        // set the camera position
        Camera.main.transform.position = cameraPos;
    }
}
