using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionListener : MonoBehaviour
{
    [SerializeField] int damageAmount = 1;
    private ParticleSystem part;
    private List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        for (int i = 0; i < numCollisionEvents; i++)
        {
            SendDamageMessage(other);
        }
    }
    void SendDamageMessage(GameObject other)
    {
        Debug.Log("Collide Particle name: " + other.name);
        Enemy d = other.GetComponent<Enemy>();
        if (d == null)
            return;

        d.myStats.TakeDamage(damageAmount);

        //var msg = new Damageable.DamageMessage()
        //{
        //    amount = damageAmount,
        //    damager = this,
        //    direction = Vector3.up,
        //    stopCamera = false
        //};

        //d.ApplyDamage(msg);
    }
}
