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
    public GameStart Game;
    public GameObject Canvas;

    private GameObject lazer;
    private bool lazerWorking = false;

    private Dictionary<string, string> validElements = new Dictionary<string, string>();
    private Light alarmLight;
    private AudioSource audioSource;
    private AudioSource audioSourceAI;
    private string elementMade = null;
    private bool elementSpoken = false;
    private int alarmLightFlashes = 0;

    public Dictionary<string, Element> Elements = new Dictionary<string, Element>();

    private void Start()
    {
        using var reader = new StreamReader("Assets/elements.csv");
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var records = csv.GetRecords<Element>();
        foreach (var record in records)
        {
            validElements.Add($"p:{record.Protons}|n:{record.Neutrons}|e:{record.Electrons}", record.Symbol.ToLower());
            Elements.Add(record.Symbol.Trim().ToLower(), record);
        }

        // get child point light object
        var pointLight = transform.GetChild(1).gameObject;

        alarmLight = pointLight.GetComponent<Light>();

        // get audio source component
        audioSource = GetComponent<AudioSource>();
        audioSourceAI = AI.GetComponent<AudioSource>();
    }

    private void Update()
    {        
        if (elementSpoken && !audioSourceAI.isPlaying)
        {
            elementSpoken = false;
            elementMade = null;
        }
        
        if (!audioSourceAI.isPlaying && elementMade != null)
        {
            audioSourceAI.clip = Resources.Load<AudioClip>($"Elements/{elementMade}");
            audioSourceAI.Play();
            elementSpoken = true;
        }
        
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
                if (validElements.ContainsKey(key) && ((!Game.GameStarted) || (Game.GameStarted && (validElements[key] == Game.TargetElement))))
                {
                    elementMade = validElements[key];
                    CheckInside.DestroyObjects(true);
                    GenerateAtomBase();
                    lazerWorking = false;
                    alarmLightFlashes = 10;
                    alarmLight.color = Color.green;
                    audioSourceAI.clip = Resources.Load<AudioClip>("SuccessMessage");
                    audioSourceAI.Play();
                    if (Game.GameStarted)
                    {
                        Canvas.GetComponent<PanelSelector>().EndGame("Challenge Completed Successfully");
                    }
                }
                else
                {
                    alarmLightFlashes = 10;
                    alarmLight.color = Color.red;
                    audioSource.clip = Resources.Load<AudioClip>("alarm");
                    audioSource.Play();
                    audioSourceAI.clip = Resources.Load<AudioClip>("FailureMessage");
                    audioSourceAI.Play();
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
        var obj = Instantiate(AtomBase, transform.position + new Vector3(0, -6f, 0f), Quaternion.identity);

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