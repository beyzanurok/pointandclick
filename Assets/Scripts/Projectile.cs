using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private int damage;
    [SerializeField] private float lifetime = 5.0f;

    private Character owner;

    private Rigidbody rig;

    void Start ()
    {
        rig = GetComponent<Rigidbody>();
        rig.velocity = transform.forward * moveSpeed;

        Destroy(gameObject, lifetime);
    }

    public void Setup (Character character)
    {
        owner = character;
    }

    void OnTriggerEnter (Collider other)
    {
        Character hit = other.GetComponent<Character>();

        if(hit != owner && hit != null)
        {
            hit.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}