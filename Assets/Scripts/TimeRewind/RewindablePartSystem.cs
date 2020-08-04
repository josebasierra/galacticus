using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindablePartSystem 
{
    struct TimeCutParticle
    {
        public Vector3 position;
        public Vector3 velocity;
    }

    Rewindable rewindable;
    ParticleSystem pSystem;
    ParticleSystemRenderer pSystemRenderer;

    LimitedStack<TimeCutParticle[]> particlesRegister;
    ParticleSystem.Particle[] particleBuffer;

    Material defaultMaterial;


    public RewindablePartSystem(Rewindable _rewindable, ParticleSystem _pSystem)
    {
        rewindable = _rewindable;
        pSystem = _pSystem;

        particlesRegister = new LimitedStack<TimeCutParticle[]>(rewindable.MaxCapacity());
        particleBuffer = new ParticleSystem.Particle[pSystem.main.maxParticles];

        rewindable.OnRewind += OnRewind;
        rewindable.OnRecord += OnRecord;

        pSystemRenderer = pSystem.GetComponent<ParticleSystemRenderer>();
        defaultMaterial = pSystemRenderer.material;
    }

    ~RewindablePartSystem()
    {
        rewindable.OnRewind -= OnRewind;
        rewindable.OnRecord -= OnRecord;
    }


    void OnRewind(float time)
    {
        var timeCutParticles = particlesRegister.Top();
        for (int i = 0; i < timeCutParticles.Length; i++)
        {
            particleBuffer[i].position = timeCutParticles[i].position;
            particleBuffer[i].velocity = timeCutParticles[i].velocity;
        }
        pSystem.SetParticles(particleBuffer, timeCutParticles.Length);

        particlesRegister.Pop();

        pSystemRenderer.sharedMaterial = GameManager.Instance().GetHighlightMaterial();
    }


    void OnRecord()
    {
        int particleCount = pSystem.GetParticles(particleBuffer);

        var timeCutParticles = new TimeCutParticle[particleCount];
        for (int i = 0; i < pSystem.particleCount; i++)
        {
            timeCutParticles[i].position = particleBuffer[i].position;
            timeCutParticles[i].velocity = particleBuffer[i].velocity;
        }

        particlesRegister.Push(timeCutParticles);

        pSystemRenderer.sharedMaterial = defaultMaterial;
    }

  
}
