using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Vector3 movementVector;

    [SerializeField] private float speed = 3f;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

    }

    // Start is called before the first frame update
    void Start()
    {
        //Starting code
    }

    // Update is called once per frame
    void Update()
    {
        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.y = Input.GetAxis("Vertical");

        movementVector *= speed;

        _rigidbody2D.velocity = movementVector;
    }
}
