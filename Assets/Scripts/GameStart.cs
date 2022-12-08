using System;
using System.Linq;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public AtomBaseGenerator AtomBaseGenerator;
    public GameObject ProtonsSphere;
    public GameObject NeutronsSphere;
    public GameObject ElectronsSphere;
    public GameObject Canvas;
    public GameObject AI;

    private AudioSource audioSourceAI;
    private TMPro.TextMeshProUGUI element;
    private TMPro.TextMeshProUGUI time;
    private float timerValue = 0;
    public bool GameStarted = false;
    public string TargetElement;

    // Start is called before the first frame update
    void Start()
    {
        var textBoxes = GetComponentsInChildren<TMPro.TextMeshProUGUI>();
        foreach (var textBox in textBoxes)
        {
            switch (textBox.name)
            {
                case "Element":
                    element = textBox;
                    break;
                case "Seconds":
                    time = textBox;
                    break;
            }
        }
        
        audioSourceAI = AI.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerValue > 0)
        {
            timerValue -= Time.deltaTime;
            time.text = timerValue.ToString("0.00");
        }
        else if (GameStarted && timerValue <= 0)
        {
            Canvas.GetComponent<PanelSelector>().EndGame("Challenge Failed");
        }
        else
        {
            time.text = "0.00";
        }
    }

    public void StartGame()
    {
        int randomNumber = UnityEngine.Random.Range(0, 50);
        var randomKey = AtomBaseGenerator.Elements.Keys.ToList()[randomNumber];
        var randomElement = AtomBaseGenerator.Elements[randomKey];

        ProtonsSphere.GetComponent<DestroyChildren>().DestroyChildObjects();
        NeutronsSphere.GetComponent<DestroyChildren>().DestroyChildObjects();
        ElectronsSphere.GetComponent<DestroyChildren>().DestroyChildObjects();

        ProtonsSphere.GetComponent<ParticleGenerator>().Generate(randomElement.Protons*2);
        NeutronsSphere.GetComponent<ParticleGenerator>().Generate(randomElement.Neutrons*2);
        ElectronsSphere.GetComponent<ParticleGenerator>().Generate(randomElement.Electrons*2);
        element.text = randomElement.ElementName;
        timerValue = Math.Max(randomElement.Protons, randomElement.Neutrons) * 5;
        GameStarted = true;
        TargetElement = randomElement.Symbol.ToLower();
        audioSourceAI.clip = Resources.Load<AudioClip>($"Elements/{randomElement.Symbol.ToLower()}");
        audioSourceAI.Play();
    }
}
