using UnityEngine;
using System.Collections.Generic;

public class CheckInside : MonoBehaviour
{
    public int Protons = 0;
    public int Neutrons = 0;
    public int Electrons = 0;

    private List<string> unWanted = new List<string>() { "Cube", "Cylinder" };

    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position, transform.localScale / 4);
        int protons = 0;
        int neutrons = 0;
        int electrons = 0;
        if (hitColliders.Length > 0)
        {
            foreach (Collider collider in hitColliders)
            {
                if (unWanted.Contains(collider.gameObject.name))
                {
                    continue;
                }
                switch (collider.gameObject.name)
                {
                    case "proton":
                    case "proton(Clone)":
                        protons += 1;
                        break;
                    case "neutron":
                    case "neutron(Clone)":
                        neutrons += 1;
                        break;
                    case "electron":
                    case "electron(Clone)":
                        electrons += 1;
                        break;
                }
            }

            Protons = protons;
            Neutrons = neutrons;
            Electrons = electrons;
        }
    }
}