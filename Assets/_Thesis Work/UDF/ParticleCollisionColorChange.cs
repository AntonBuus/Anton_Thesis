using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleCollisionColorChange : MonoBehaviour
{
    private ParticleSystem ps;
    private ParticleSystem.Particle[] particles;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        int maxParticles = ps.main.maxParticles;

        if (particles == null || particles.Length < maxParticles)
            particles = new ParticleSystem.Particle[maxParticles];

        int numParticlesAlive = ps.GetParticles(particles);

        List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();
        int eventCount = ps.GetCollisionEvents(other, collisionEvents);

        for (int i = 0; i < eventCount; i++)
        {
            Vector3 collisionPos = collisionEvents[i].intersection;

            // Find the closest particle to this collision
            float minDist = float.MaxValue;
            int closestIndex = -1;

            for (int j = 0; j < numParticlesAlive; j++)
            {
                float dist = Vector3.Distance(particles[j].position, collisionPos);
                if (dist < minDist)
                {
                    minDist = dist;
                    closestIndex = j;
                }
            }

            if (closestIndex != -1)
            {
                // Change particle color to yellow
                particles[closestIndex].startColor = Color.yellow;

                // Debug "ID" (index acts as pseudo-ID)
                Debug.Log($"Particle hit! Index (ID): {closestIndex} collided with {other.name}");
            }
        }

        ps.SetParticles(particles, numParticlesAlive);
    }
}
