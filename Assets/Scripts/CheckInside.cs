using UnityEngine;
using System.Collections.Generic;

public class CheckInside : MonoBehaviour
{
    public int Protons = 0;
    public int Neutrons = 0;
    public int Electrons = 0;

    private List<string> unWanted = new List<string>() { "Cube", "Cylinder" };

    private bool destroyObjects = false;

    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position, transform.localScale / 4);
        int protons = 0;
        int neutrons = 0;
        int electrons = 0;
        if (hitColliders.Length > 0)
        {
            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (unWanted.Contains(hitColliders[i].gameObject.name))
                {
                    continue;
                }
                switch (hitColliders[i].gameObject.name)
                {
                    case "proton":
                    case "proton(Clone)":
                        if (destroyObjects)
                        {
                            Destroy(hitColliders[i].gameObject);
                        }
                        else
                        {
                            protons += 1;
                        }
                        break;
                    case "neutron":
                    case "neutron(Clone)":
                        if (destroyObjects)
                        {
                            Destroy(hitColliders[i].gameObject);
                        }
                        else
                        {
                            neutrons += 1;
                        }
                        break;
                    case "electron":
                    case "electron(Clone)":
                        if (destroyObjects)
                        {
                            Destroy(hitColliders[i].gameObject);
                        }
                        else
                        {
                            electrons += 1;
                        }
                        break;
                }
            }

            Protons = protons;
            Neutrons = neutrons;
            Electrons = electrons;
        }
    }

    public void DestroyObjects(bool destroy)
    {
        destroyObjects = true;
    }

    public void ReadyForNewObjects()
    {
        destroyObjects = false;
    }
}