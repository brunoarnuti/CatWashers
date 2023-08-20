using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class CatPusher : MonoBehaviour
{
    public float pushForce = 10f; // Force to push cats away

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("gatos"))
        {
            Vector2 pushDirection = (collision.transform.position - transform.position).normalized;
            collision.GetComponent<Rigidbody2D>().AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
        }
    }
}
