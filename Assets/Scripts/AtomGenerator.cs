using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class AtomGenerator : MonoBehaviour
{
    public GameObject proton;
    public int protonCount = 2;
    
    public GameObject neutron;
    public int neutronCount = 2;

    public GameObject electron;
    public int electronCount = 2;
    
    void Start()
    {
        ParticleGenerator(proton, protonCount, 0.3f);
        ParticleGenerator(neutron, neutronCount, 0.3f);
        ParticleGenerator(electron, electronCount, 0.1f, 0.4f);
    }
    
    private void ParticleGenerator(GameObject particle, int count, float scale, float radius = 0.2f)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newParent = null;
            if (particle == electron)
            {
                var parent = new GameObject("nucleus");
                parent.transform.localPosition = Vector3.zero;
                parent.transform.localRotation = Random.rotation;
                

                // instatiate parent
                newParent = Instantiate(parent, transform);
            }

            var obj = Instantiate(particle, newParent == null ? transform : newParent.transform);
            obj.transform.localPosition = Random.insideUnitSphere * radius;
            obj.transform.localRotation = Random.rotation;

            // increase size of particle
            obj.transform.localScale = new Vector3(scale, scale, scale);
            Destroy(obj.GetComponent<RandomMotion>());

            // add orbit script to particle
            if (particle == electron)
            {
                var orbit = obj.AddComponent<Orbit>();
                orbit.a = Random.Range(0.5f, 1.5f);
                orbit.b = Random.Range(0.5f, 1.5f);
                orbit.speed = Random.Range(0.5f, 1.5f);
                orbit.angleOffset = Random.Range(0f, 360f);
            }
        }
    }
}
