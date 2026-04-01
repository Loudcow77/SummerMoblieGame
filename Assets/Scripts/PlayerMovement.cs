using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rigid;
    float dirX;
    float moveSpeed = 3f;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        dirX = Input.acceleration.x * moveSpeed;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -7.2f, 7.2f), transform.position.y, transform.position.z);
        dirX = Input.GetAxis("Horizontal") * moveSpeed;
    }

    private void FixedUpdate()
    {
        rigid.velocity = new Vector3(dirX, 0f, 0f);
    }

    // Checks if the player was hit by a block
    private void OnCollisionEnter(Collision collision)
    {
        // If player was hit end the game
        if (collision.gameObject.tag == "DodgeBlock")
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }
}
