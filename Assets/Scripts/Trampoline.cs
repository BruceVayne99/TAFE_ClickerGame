using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public Rigidbody2D rb;
    public float launchForce = 1;

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Trampoline"))
        {
            var rnd = Random.insideUnitCircle;
            rb.velocity = new Vector2(rnd.x, rnd.y) * launchForce;
        }
    }
}
