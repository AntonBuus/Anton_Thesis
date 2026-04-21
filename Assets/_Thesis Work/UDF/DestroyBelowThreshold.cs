using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBelowThreshold : MonoBehaviour
{
    public float killY = -1f;
    private ParticleSystem ps;

    private ParticleSystem.Particle[] particles;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[ps.main.maxParticles];
    }

    void LateUpdate()
    {
        int count = ps.GetParticles(particles);

        for (int i = 0; i < count; i++)
        {
            if (particles[i].position.y < killY)
            {
                particles[i].remainingLifetime = 0;
            }
        }

        ps.SetParticles(particles, count);
    }
}
