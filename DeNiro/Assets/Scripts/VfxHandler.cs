using System.Collections.Generic;
using UnityEngine;

public class VfxHandler : MonoBehaviour
{
    [SerializeField]
    private List<ParticleSystem> m_particles;

    public void LaunchParticles()
    {
        foreach (var particle in m_particles)
        {
            particle.Play();
        }
    }
}
