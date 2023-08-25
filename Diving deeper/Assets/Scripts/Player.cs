using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private float xBound1=11.9f;
    private float xBound2 = -7.9f;
    public float yBound1=3.9f;
    public float yBound2=-35;
    public float speed = 5.0f;
    public float rotationSpeed = 5.0f;
    private Quaternion initialRotation;
    private Vector2 movement;
    private float rotationAngle = 45f;
    private Quaternion targetRotation = Quaternion.identity;
    private Rigidbody2D rb;
    [SerializeField]private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        initialRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //get input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //movement direction vector
        movement = new Vector2(horizontal, vertical);

        //checking movement direction
        float localScaleX = Mathf.Abs(transform.localScale.x);
        if (horizontal > 0)
        {
            targetRotation = Quaternion.Euler(0, 0, rotationAngle);
            transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
        }
        if (horizontal < 0)
        {
            targetRotation = Quaternion.Euler(0, 0, -rotationAngle);
            transform.localScale = new Vector3(-localScaleX, transform.localScale.y, transform.localScale.z);
        }
        if (horizontal == 0f)
        {
            targetRotation = initialRotation;
        }

        //checking bounds and keeping the player inside bound
        if(transform.position.x >= xBound1)
            transform.position = new Vector3(xBound1, transform.position.y, 0);
        if (transform.position.x <= xBound2)
            transform.position = new Vector3(xBound2, transform.position.y, 0);
        if (transform.position.y >= yBound1)
            transform.position = new Vector3(transform.position.x, yBound1, 0);
        if (transform.position.y <= yBound2)
            transform.position = new Vector3(transform.position.x, yBound2, 0);
    }

    private void FixedUpdate()
    {
        //check for movement direction and move the player
        if (movement != null)
        {
            rb.velocity = movement.normalized * speed;
        }

        //rotate the player
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed);
        rb.MoveRotation(rotation);

    }
}