using UnityEngine;

public class DetailPanelGenerate : MonoBehaviour
{
    public GameObject Details;
    public AtomBaseGenerator AtomBaseGenerator;

    public void GenerateDetails(string symbol)
    {
        var element = AtomBaseGenerator.Elements[symbol];

        GameObject detail = Instantiate(Details, transform);
        detail.transform.SetParent(transform);

        // get all children text components
        var textsBoxes = detail.GetComponentsInChildren<TMPro.TextMeshProUGUI>();

        foreach (var textBox in textsBoxes)
        {
            switch (textBox.name)
            {
                case "Name":
                    textBox.text = element.ElementName.ToString();
                    break;
                case "AtomicNumberValue":
                    textBox.text = element.AtomicNumber.ToString();
                    break;
                case "AtomicMassValue":
                    textBox.text = element.AtomicMass.ToString();
                    break;
                case "ElectronCount":
                    textBox.text = element.Electrons.ToString();
                    break;
                case "NeutronCount":
                    textBox.text = element.Neutrons.ToString();
                    break;
                case "ProtonCount":
                    textBox.text = element.Protons.ToString();
                    break;
                case "DensityValue":
                    textBox.text = element.Density.ToString();
                    break;
                case "MeltingPointValue":
                    textBox.text = element.MeltingPoint.ToString();
                    break;
                case "BoilingPointValue":
                    textBox.text = element.BoilingPoint.ToString();
                    break;
            }
        }
    }
}
