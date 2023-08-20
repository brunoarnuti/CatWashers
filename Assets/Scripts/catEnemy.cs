using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catEnemy : MonoBehaviour
{
    Transform targetDestination;
    [SerializeField] float speed;

    GameObject targetGameObject;
    Rigidbody2D catRigibody2D;

    private void Awake()
    {
        catRigibody2D = GetComponent<Rigidbody2D>();
    }

    public void SetTarget(GameObject target)
    {
        targetGameObject = target;
        targetDestination = target.transform;
    }

    private void FixedUpdate()
    {
        Vector3 direction = (targetDestination.position - transform.position).normalized;
        catRigibody2D.velocity = direction * speed;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == targetGameObject) //TODO: ver si hay que suplantar con otro script
        {
            Attack();
        }
    }

    private void Attack()
    {
       // Debug.Log("Attacking the character!");
    }
}
