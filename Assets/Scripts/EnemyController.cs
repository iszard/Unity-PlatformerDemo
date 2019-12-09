using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Speed of the enemy
    public float speed = 3;

    // range of movement Y
    public float rangeY = 2;

    // Inital position
    Vector3 initalPos;

    // direction
    int direction = 1;
 

    // Start is called before the first frame update
    void Start()
    {
        // save initial position
        initalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // factor
        float factor = direction==-1?2:1;

        // how much are we moving?
        float movementY = factor * speed * Time.deltaTime * direction;

        // new position y
        float newY = transform.position.y + movementY;

        if(Mathf.Abs(newY - initalPos.y) > rangeY) {
            direction *= -1;
        } else {
            transform.position += new Vector3(0, movementY, 0);
        }
    }
}
