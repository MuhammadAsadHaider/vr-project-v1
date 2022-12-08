using UnityEngine;

public class DetailPanelGenerate : MonoBehaviour
{
    public GameObject Table;
    
    public GameObject Details;
    public GameObject AI;
    
    public AtomBaseGenerator AtomBaseGenerator;

    private AudioSource audioSource;

    private void Start()
    {
        // get audio source
        audioSource = AI.GetComponent<AudioSource>();
    }

    public void GenerateDetails(string symbol)
    {
        if (!AtomBaseGenerator.Elements.ContainsKey(symbol.Trim()))
        {
            Debug.Log($"{symbol} not found");
            return;
        }

        audioSource.clip = Resources.Load<AudioClip>($"Elements/{symbol.Trim()}");
        audioSource.Play();
        var element = AtomBaseGenerator.Elements[symbol.Trim()];

        // check if child named Detail clone exists
        if (transform.Find("Details(Clone)") != null)
        {
            // destroy the atom base
            Destroy(transform.Find("Details(Clone)").gameObject);
        }

        GameObject detail = Instantiate(Details, transform);
        detail.transform.SetParent(transform);

        // get all children text components
        var textsBoxes = detail.GetComponentsInChildren<TMPro.TextMeshProUGUI>();

        // get all button children
        var buttons = detail.GetComponentsInChildren<UnityEngine.UI.Button>();

        // set button on click event
        foreach (var button in buttons)
        {
            button.onClick.AddListener(() => { Table.GetComponent<DestroyChildren>().DestroyChildObjects(); });
        }

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
