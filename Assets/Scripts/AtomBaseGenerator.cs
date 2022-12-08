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
    public GameObject AI;

    private GameObject lazer;
    private bool lazerWorking = false;

    private HashSet<string> validElements;
    private Light alarmLight;
    private AudioSource audioSource;
    private AudioSource audioSourceAI;

    public Dictionary<string, Element> Elements = new Dictionary<string, Element>();

    private void Start()
    {
        var reader = new StreamReader("Assets/elements.csv");
        var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<Element>();
        validElements = new HashSet<string>();
        foreach (var record in records)
        {
            validElements.Add($"p:{record.Protons}|n:{record.Neutrons}|e:{record.Electrons}");
            Elements.Add(record.Symbol.Trim().ToLower(), record);
        }

        // get child point light object
        var pointLight = transform.GetChild(1).gameObject;

        alarmLight = pointLight.GetComponent<Light>();

        // get audio source component
        audioSource = GetComponent<AudioSource>();
        audioSourceAI = AI.GetComponent<AudioSource>();
    }

    private int alarmLightFlashes = 0;

    private void Update()
    {
        if (alarmLightFlashes > 0)
        {
            if (Time.frameCount % 25 == 0)
            {
                alarmLight.intensity = alarmLight.intensity == 0.0f ? 1.0f : 0.0f;
                alarmLightFlashes--;
            }
        }
        else
        {
            audioSource.Stop();
            alarmLight.intensity = 0.0f;
        }

        if (lazer != null)
        {
            // increase lazer size linearly
            lazer.transform.localScale += new Vector3(0.01f, 0f, 0.01f);

            // when local scale reaches 4, destroy lazer
            if (lazer.transform.localScale.x >= 3)
            {
                Destroy(lazer);
                string key = $"p:{CheckInside.Protons}|n:{CheckInside.Neutrons}|e:{CheckInside.Electrons}";
                if (validElements.Contains(key))
                {
                    CheckInside.DestroyObjects(true);
                    GenerateAtomBase();
                    lazerWorking = false;
                    alarmLightFlashes = 6;
                    alarmLight.color = Color.green;
                    
                    
                }
                else
                {
                    alarmLightFlashes = 6;
                    alarmLight.color = Color.red;
                    audioSource.clip = Resources.Load<AudioClip>("alarm");
                    audioSource.Play();
                    Debug.Log("No such element");
                    lazerWorking = false;
                }
            }
        }
    }

    public void TryGenerateAtom()
    {
        if (!lazerWorking)
        {
            lazer = Instantiate(Lazer, transform.position + new Vector3(0, -8f, 0f), Quaternion.identity);
            lazerWorking = true;
        }
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

    public class Element
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
        public float AtomicMass { get; set; }

        [CsvHelper.Configuration.Attributes.Index(7)]
        public string StateAtRoomTemp { get; set; }

        [CsvHelper.Configuration.Attributes.Index(8)]
        public string MeltingPoint { get; set; }

        [CsvHelper.Configuration.Attributes.Index(9)]
        public string BoilingPoint { get; set; }

        [CsvHelper.Configuration.Attributes.Index(10)]
        public string Density { get; set; }
    }
}