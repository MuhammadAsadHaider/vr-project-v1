// generate 10 child objects with random position and rotation inside parent object


using UnityEngine;

public class ParticleGenerator : MonoBehaviour
{
    public GameObject prefab;
    public int count = 100;
    public float scale = 0.3f;

    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            var obj = Instantiate(prefab, transform);
            obj.transform.localPosition = Random.insideUnitSphere * scale;
            obj.transform.localRotation = Random.rotation;
        }
    }
    
    public void Generate(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var obj = Instantiate(prefab, transform);
            obj.transform.localPosition = Random.insideUnitSphere * scale;
            obj.transform.localRotation = Random.rotation;
        }
    }
}