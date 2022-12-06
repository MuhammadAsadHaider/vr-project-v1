using UnityEngine;

public class AtomGenerator : MonoBehaviour
{
    public GameObject Proton;
    
    public GameObject Neutron;

    public GameObject Electron;

    // function to generate particle
    public void ParticleGeneratorInit(int protonCount, int neutronCount, int electronCount)
    {
        ParticleGenerator(Proton, protonCount, 0.3f);
        ParticleGenerator(Neutron, neutronCount, 0.3f);
        ParticleGenerator(Electron, electronCount, 0.1f, 0.4f);
    }

    private void ParticleGenerator(GameObject particle, int count, float scale, float radius = 0.2f)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject newParent = null;
            if (particle == Electron)
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
            if (particle == Electron)
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
