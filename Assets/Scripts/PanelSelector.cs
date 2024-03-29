using System.Linq;
using UnityEngine;

public class PanelSelector : MonoBehaviour
{
    public GameObject AI;
    
    private GameObject startScreen;
    private GameObject gameScreen;
    private GameObject endScreen;
    private AudioSource audioSourceAI;

    private bool gameStarted = false;
    private bool gameInititated = false;

    // Start is called before the first frame update
    void Start()
    {
        // loop through children
        foreach (Transform child in transform)
        {
            // if child is a panel
            if (child.name.Contains("StartScreen"))
            {
                // set it to inactive
                startScreen = child.gameObject;
            }
            else if (child.name.Contains("Game"))
            {
                gameScreen = child.gameObject;
            }
            else if (child.name.Contains("End"))
            {
                endScreen = child.gameObject;
            }
        }
        
        audioSourceAI = AI.GetComponent<AudioSource>();
    }

    void update()
    {
        if (gameStarted && !gameInititated)
        {
            gameScreen.GetComponent<GameStart>().StartGame();
            gameInititated = true;
        }
    }

    public void PlayInstructions()
    {
        audioSourceAI.clip = Resources.Load<AudioClip>("ChallengeInstructions");
        audioSourceAI.Play();
    }

    public void Home()
    {
        gameScreen.GetComponent<GameStart>().GameStarted = false;
        gameStarted = false;

        endScreen.SetActive(false);
        startScreen.SetActive(true);
        gameScreen.SetActive(false);
    }

    public void StartGame()
    {
        endScreen.SetActive(false);
        startScreen.SetActive(false);
        gameScreen.SetActive(true);

        gameStarted = true;
    }

    public void EndGame(string message)
    {
        gameScreen.GetComponent<GameStart>().GameStarted = false;
        gameScreen.SetActive(false);
        endScreen.SetActive(true);
        gameStarted = false;
        var textBoxes = GetComponentsInChildren<TMPro.TextMeshProUGUI>();
        var textBox = textBoxes.Where(x => x.name == "SuccessOrFail").FirstOrDefault();
        textBox.text = message;
    }
}
