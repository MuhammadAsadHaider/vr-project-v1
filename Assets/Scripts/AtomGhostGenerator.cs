using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class AtomGhostGenerator : MonoBehaviour
{
    public GameObject AtomBase;
    public AtomBaseGenerator AtomBaseGenerator;

    // function to generate atom base
    public void GenerateAtomBase(string symbol)
    {
        if (!AtomBaseGenerator.Elements.ContainsKey(symbol.Trim()))
        {
            Debug.Log($"{symbol} not found");
            return;
        }

        var element = AtomBaseGenerator.Elements[symbol.Trim()];

        // check if atom base already exists as a child
        if (transform.childCount > 0)
        {
            // destroy the atom base
            Destroy(transform.GetChild(0).gameObject);
        }

        // initiate atome base to be inside parent
        var obj = Instantiate(AtomBase, transform.position + new Vector3(0, 2f, 0f), Quaternion.identity);

        // add atom to parent
        obj.transform.parent = transform;

        Destroy(obj.GetComponent<XRGrabInteractable>());

        // add component to atombase
        var generator = obj.GetComponent<AtomGenerator>();
        generator.ParticleGeneratorInit(element.Protons, element.Neutrons, element.Electrons, 0.5f);
    }
}