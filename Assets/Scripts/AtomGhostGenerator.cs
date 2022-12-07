using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class AtomGhostGenerator : MonoBehaviour
{
    public GameObject AtomBase;
    public AtomBaseGenerator AtomBaseGenerator;

    // function to generate atom base
    public void GenerateAtomBase(string symbol)
    {
        var element = AtomBaseGenerator.Elements[symbol];

        Debug.Log($"Element: {element.ElementName}");
        // initiate atome base to be inside parent
        var obj = Instantiate(AtomBase, transform.position + new Vector3(0, 2f, 0f), Quaternion.identity);

        // add atom to parent
        obj.transform.parent = transform;

        Destroy(obj.GetComponent<XRGrabInteractable>());

        // add component to atombase
        var generator = obj.GetComponent<AtomGenerator>();
        generator.ParticleGeneratorInit(element.Protons, element.Neutrons, element.Electrons, 0.2f);
    }
}