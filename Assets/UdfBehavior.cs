using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class UdfBehavior : MonoBehaviour
{
    [SerializeField] private Color _collisionTrailColor = Color.cyan;
    [SerializeField] private float _collisionRadius = 0.15f;

    ParticleSystem _UDFparticleSystem;
    readonly List<ParticleCollisionEvent> _collisionEvents = new List<ParticleCollisionEvent>();
    readonly HashSet<uint> _collidedParticleSeeds = new HashSet<uint>();
    ParticleSystem.Particle[] _particles = new ParticleSystem.Particle[0];
    readonly HashSet<uint> _aliveSeedsBuffer = new HashSet<uint>();

    void Awake()
    {
        _UDFparticleSystem = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        // Guard clause: if the ParticleSystem reference is missing, we cannot read collisions or update particles.
        // Returning early avoids null-reference errors and unnecessary work.
        if (_UDFparticleSystem == null)
        {
            return;
        }

        // Collect collision events generated against the other object.
        // If there are no events, there is nothing to recolor, so we exit early for performance.
        int eventCount = ParticlePhysicsExtensions.GetCollisionEvents(_UDFparticleSystem, other, _collisionEvents);
        if (eventCount == 0)
        {
            return;
        }

        // Read currently alive particles so we can modify their startColor.
        // Ensure the backing array is large enough; if not, resize and fetch again to avoid truncation.
        int particleCount = _UDFparticleSystem.GetParticles(_particles);
        if (_particles.Length < particleCount)
        {
            _particles = new ParticleSystem.Particle[particleCount];
            particleCount = _UDFparticleSystem.GetParticles(_particles);
        }

        // Precompute squared collision radius once so distance checks can use sqrMagnitude.
        // This avoids repeated square-root operations and is faster in tight loops.
        float collisionRadiusSqr = _collisionRadius * _collisionRadius;

        ParticleSystem.MainModule main = _UDFparticleSystem.main;

        // For each collision point, scan particles and mark those within radius.
        // We keep their seed so color can persist until each particle naturally dies.
        for (int i = 0; i < eventCount; i++)
        {
            Vector3 collisionPoint = _collisionEvents[i].intersection;

            for (int p = 0; p < particleCount; p++)
            {
                Vector3 particleWorldPos = GetParticleWorldPosition(_particles[p].position, main);

                if ((particleWorldPos - collisionPoint).sqrMagnitude <= collisionRadiusSqr)
                {
                    _collidedParticleSeeds.Add(_particles[p].randomSeed);
                }
            }
        }
    }

    void LateUpdate()
    {
        if (_UDFparticleSystem == null || _collidedParticleSeeds.Count == 0)
        {
            return;
        }

        int particleCount = _UDFparticleSystem.GetParticles(_particles);
        if (_particles.Length < particleCount)
        {
            _particles = new ParticleSystem.Particle[particleCount];
            particleCount = _UDFparticleSystem.GetParticles(_particles);
        }

        _aliveSeedsBuffer.Clear();

        for (int i = 0; i < particleCount; i++)
        {
            uint seed = _particles[i].randomSeed;
            _aliveSeedsBuffer.Add(seed);

            if (_collidedParticleSeeds.Contains(seed))
            {
                Color32 existing = _particles[i].startColor;
                Color32 tint = _collisionTrailColor;
                tint.a = existing.a;
                _particles[i].startColor = tint;
            }
        }

        _UDFparticleSystem.SetParticles(_particles, particleCount);

        _collidedParticleSeeds.IntersectWith(_aliveSeedsBuffer);
    }

    private Vector3 GetParticleWorldPosition(Vector3 particlePosition, ParticleSystem.MainModule main)
    {
        switch (main.simulationSpace)
        {
            case ParticleSystemSimulationSpace.World:
                return particlePosition;

            case ParticleSystemSimulationSpace.Local:
                return transform.TransformPoint(particlePosition);

            case ParticleSystemSimulationSpace.Custom:
                if (main.customSimulationSpace != null)
                {
                    return main.customSimulationSpace.TransformPoint(particlePosition);
                }
                return transform.TransformPoint(particlePosition);

            default:
                return transform.TransformPoint(particlePosition);
        }

    }
}
