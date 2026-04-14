using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleCollisionColorChange : MonoBehaviour
{
    private ParticleSystem ps;
    private ParticleSystem.Particle[] particles;
    [SerializeField] private List<int> collidedParticleIndices = new List<int>();

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
        collidedParticleIndices.Clear();

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
                if (!collidedParticleIndices.Contains(closestIndex))
                    collidedParticleIndices.Add(closestIndex);
            }
        }

        for (int i = 0; i < collidedParticleIndices.Count; i++)
        {
            // if (other.name != "Product")
            // {
            //     continue;
            // }
            int particleIndex = collidedParticleIndices[i];
            Debug.Log($"Particle Color Before: {particles[particleIndex].startColor}");
            particles[particleIndex].startColor = Color.yellow;
            Debug.Log($"Particle hit! Index (ID): {particleIndex} collided with {other.name}");
            Debug.Log($"Particle Color After: {particles[particleIndex].startColor}");
        }

        ps.SetParticles(particles, numParticlesAlive);
    }
}
