using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleCollisionColorChange : MonoBehaviour
{
    private ParticleSystem ps;
    private ParticleSystem.Particle[] particles;
    [SerializeField] private List<int> collidedParticleIndices = new List<int>();

    public static bool product1Contaminated = false;
    public static bool product2Contaminated = false;
    public static bool product3Contaminated = false;
    public static bool product4Contaminated = false;
    public static bool product5Contaminated = false;
    public static bool product6Contaminated = false;
    

    TrackContamination _trackContaminationScript;
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        // if scenename is "EvaluationModule_Scene", then find the TrackContamination script
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "EvaluationModule")
        {
            _trackContaminationScript = GameObject.Find("Product_dishes").GetComponent<TrackContamination>();
            Debug.Log("TrackContamination script found and assigned in ParticleCollisionColorChange.");
        }
        //restart product contamination status
        product1Contaminated = false;
        product2Contaminated = false;
        product3Contaminated = false;
        product4Contaminated = false;
        product5Contaminated = false;
        product6Contaminated = false;
        
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
            Vector3 collisionPosition = collisionEvents[i].intersection;

            // Find the closest particle to this collision
            float minDist = float.MaxValue;
            int closestIndex = -1;

            for (int j = 0; j < numParticlesAlive; j++)
            {
                float distance = Vector3.Distance(particles[j].position, collisionPosition);
                if (distance < minDist)
                {
                    minDist = distance;
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
            if (other.tag != "_product")
            {
                int particleIndex = collidedParticleIndices[i];
                Debug.Log($"Particle Color Before: {particles[particleIndex].startColor}");
                // if (particles[particleIndex].startColor != Color.yellow)
                particles[particleIndex].startColor = Color.yellow;
                Debug.Log($"Particle hit! Index (ID): {particleIndex} collided with {other.name}");
                Debug.Log($"Particle Color After: {particles[particleIndex].startColor}");
                
            }
            else
            {
                int productParticleIndex = collidedParticleIndices[i];
                if (particles[productParticleIndex].startColor == Color.yellow)
                {
                    // particles[productParticleIndex].startColor = Color.red;
                    Debug.Log($"Particle hit product! Index (ID): {productParticleIndex} collided with {other.name}");
                    
                    if(!product1Contaminated && other.name == "Petridish_1")
                    {
                        product1Contaminated = true;
                        Debug.Log("proudct 1 is contaminated");
                        other.transform.GetChild(1).GetComponent<Renderer>().material.color = Color.red;
                        if (_trackContaminationScript != null)
                        {
                            _trackContaminationScript._contaminatedDishesAmount++;
                        }
                    }
                    if(!product2Contaminated && other.name == "Petridish_2")
                    {
                        product2Contaminated = true;
                        Debug.Log("proudct 2 is contaminated");
                        other.transform.GetChild(1).GetComponent<Renderer>().material.color = Color.red;
                        if (_trackContaminationScript != null)
                        {
                            _trackContaminationScript._contaminatedDishesAmount++;
                        }
                    }
                    if(!product3Contaminated && other.name == "Petridish_3")
                    {
                        product3Contaminated = true;
                        Debug.Log("proudct 3 is contaminated");
                        other.transform.GetChild(1).GetComponent<Renderer>().material.color = Color.red;
                        if (_trackContaminationScript != null)
                        {
                            _trackContaminationScript._contaminatedDishesAmount++;
                        }
                    }
                    if(!product4Contaminated && other.name == "Petridish_4")
                    {
                        product4Contaminated = true;
                        Debug.Log("proudct 4 is contaminated");
                        other.transform.GetChild(1).GetComponent<Renderer>().material.color = Color.red;
                        if (_trackContaminationScript != null)
                        {
                            _trackContaminationScript._contaminatedDishesAmount++;
                        }
                    }
                    if(!product5Contaminated && other.name == "Petridish_5")
                    {
                        product5Contaminated = true;
                        Debug.Log("proudct 5 is contaminated");
                        other.transform.GetChild(1).GetComponent<Renderer>().material.color = Color.red;
                        if (_trackContaminationScript != null)
                        {
                            _trackContaminationScript._contaminatedDishesAmount++;
                        }
                    }
                    if(!product6Contaminated && other.name == "Petridish_6")
                    {
                        product6Contaminated = true;
                        Debug.Log("proudct 6 is contaminated");
                        other.transform.GetChild(1).GetComponent<Renderer>().material.color = Color.red;
                        if (_trackContaminationScript != null)
                        {
                            _trackContaminationScript._contaminatedDishesAmount++;
                        }
                    }

                }
            }   

            
            
        }

        ps.SetParticles(particles, numParticlesAlive);
    }

    // void OnParticleTrigger(GameObject other)
    // {
    //     int maxParticles = ps.main.maxParticles;

    //     if (particles == null || particles.Length < maxParticles)
    //         particles = new ParticleSystem.Particle[maxParticles];

    //     int numParticlesAlive = ps.GetParticles(particles);

    //     List<ParticleCollisionEvent> triggerEvents = new List<ParticleCollisionEvent>();
    //     int eventCount = ps.GetCollisionEvents(other, triggerEvents);
    //     collidedParticleIndices.Clear();

    //     for (int i = 0; i < eventCount; i++)
    //     {
    //         Vector3 collisionPosition = triggerEvents[i].intersection;

    //         // Find the closest particle to this collision
    //         float minDist = float.MaxValue;
    //         int closestIndex = -1;

    //         for (int j = 0; j < numParticlesAlive; j++)
    //         {
    //             float distance = Vector3.Distance(particles[j].position, collisionPosition);
    //             if (distance  < minDist)
    //             {
    //                 minDist = distance;
    //                 closestIndex = j;
    //             }
    //         }

    //         if (closestIndex != -1)
    //         {
    //             if (!collidedParticleIndices.Contains(closestIndex))
    //                 collidedParticleIndices.Add(closestIndex);
    //         }
    //     }

    //     for (int i = 0; i < collidedParticleIndices.Count; i++)
    //     {
    //         if (other.name != "Product")
    //         {
    //             continue;
    //         }
    //         Debug.Log("Trigger hit! Object: " + other.name);
    //         int particleIndex = collidedParticleIndices[i];
    //         // Debug.Log($"Particle Color Before: {particles[particleIndex].startColor}");
    //         particles[particleIndex].startColor = Color.yellow;
    //         // Debug.Log($"Particle hit! Index (ID): {particleIndex} collided with {other.name}");
    //         Debug.Log($"Particle Color After: {particles[particleIndex].startColor}");
    //     }

    //     ps.SetParticles(particles, numParticlesAlive);
    // }
    void OnParticleTrigger()
    {
        // int maxParticles = ps.main.maxParticles;

        // if (particles == null || particles.Length < maxParticles)
        //     particles = new ParticleSystem.Particle[maxParticles];

        // int numParticlesAlive = ps.GetParticles(particles);

        // for (int i = 0; i < numParticlesAlive; i++)
        // {
        //     if (particles[i].remainingLifetime > 0)
        //     {
        //         Debug.Log($"Particle Color Before: {particles[i].startColor}");
        //         particles[i].startColor = Color.yellow;
        //         Debug.Log($"Particle hit! Index (ID): {i} collided with trigger");
        //         // Debug.Log($"Particle Color After: {particles[i].startColor}");
        //     }
        // }

        // ps.SetParticles(particles, numParticlesAlive);
    }
}
