using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;

public class AtomBaseGenerator : MonoBehaviour
{
    public GameObject AtomBase;
    public CheckInside CheckInside;
    public GameObject Lazer;

    private GameObject lazer;
    
    private float i = 0;

    private bool tempDone = false;

    private Dictionary<string, Element> elements;

    private void Start()
    {
        using (var reader = new StreamReader("Assets/elements.csv"))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<Element>();
            elements = new Dictionary<string, Element>();
            foreach (var record in records)
            {
                elements.Add($"p:{record.Protons}|n:{record.Neutrons}|e:{record.Electrons}", record);
            }
        }
    }

    private void Update()
    {
        if (lazer != null)
        {
            // increase lazer size linearly
            lazer.transform.localScale += new Vector3(0.01f, 0f, 0.01f);

            // when local scale reaches 4, destroy lazer
            if (lazer.transform.localScale.x >= 3)
            {
                Destroy(lazer);
                string key = $"p:{CheckInside.Protons}|n:{CheckInside.Neutrons}|e:{CheckInside.Electrons}";
                if (elements.ContainsKey(key))
                {
                    GenerateAtomBase();
                }
                else
                {
                    Debug.Log("No such element");
                }
            }
        }
        //i += 0.01f;

        //if (i > 5 && !tempDone)
        //{
        //    TryGenerateAtom();
        //    tempDone = true;
        //}
        //if (lazer != null && tempDone)
        //{
        //    // increase lazer size linearly
        //    lazer.transform.localScale += new Vector3(0.01f, 0f, 0.01f);

        //    // when local scale reaches 4, destroy lazer
        //    if (lazer.transform.localScale.x >= 3)
        //    {
        //        Destroy(lazer);
        //        string key = $"p:{CheckInside.Protons}|n:{CheckInside.Neutrons}|e:{CheckInside.Electrons}";
        //        if (elements.ContainsKey(key))
        //        {
        //            GenerateAtomBase();
        //        }
        //        else
        //        {
        //            Debug.Log("No such element");
        //        }
        //    }
        //}
    }

    public void TryGenerateAtom()
    {
        lazer = Instantiate(Lazer, transform.position + new Vector3(0, -8f, 0f), Quaternion.identity);
    }
    
    // function to generate atom base
    private void GenerateAtomBase()
    {
        // initiate atome base to be inside parent
        var obj = Instantiate(AtomBase, transform.position + new Vector3(0, -4f, 0f), Quaternion.identity);

        // add atom to parent
        obj.transform.parent = transform;

        // add component to atombase
        var generator = obj.GetComponent<AtomGenerator>();
        generator.ParticleGeneratorInit(CheckInside.Protons, CheckInside.Neutrons, CheckInside.Electrons);
    }

    private class Element
    {        
        [CsvHelper.Configuration.Attributes.Index(0)]
        public string ElementName { get; set; }

        [CsvHelper.Configuration.Attributes.Index(1)]
        public string Symbol { get; set; }

        [CsvHelper.Configuration.Attributes.Index(2)]
        public int Electrons { get; set; }

        [CsvHelper.Configuration.Attributes.Index(3)]
        public int Protons { get; set; }

        [CsvHelper.Configuration.Attributes.Index(4)]
        public int Neutrons { get; set; }

        [CsvHelper.Configuration.Attributes.Index(5)]
        public int AtomicNumber { get; set; }

        [CsvHelper.Configuration.Attributes.Index(6)]
        public float? AtomicMass { get; set; }

        [CsvHelper.Configuration.Attributes.Index(7)]
        public string? StateAtRoomTemp { get; set; }

        [CsvHelper.Configuration.Attributes.Index(8)]
        public string? MeltingPoint { get; set; }

        [CsvHelper.Configuration.Attributes.Index(9)]
        public string? BoilingPoint { get; set; }

        [CsvHelper.Configuration.Attributes.Index(10)]
        public string? Density { get; set; }
    }
}