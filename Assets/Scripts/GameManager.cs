using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Level Settings")]
    public int killsNeeded = 10; // Set this to 10, 20, or 30 in the Inspector for each map
    private int currentKills = 0;
    private bool isGameOver = false;

    [Header("Professional UI References")]
    public TextMeshProUGUI killNumberText; // Drag the LARGE white number here
    public TextMeshProUGUI goalNumberText; // Drag the SMALL gray "/ 10 KILLS" text here

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        // Initialize the UI as soon as the map loads
        UpdateKillUI();
        UpdateGoalUI();
    }

    public void AddKill()
    {
        if (isGameOver) return;

        currentKills++;
        Debug.Log("Kills: " + currentKills + "/" + killsNeeded);
        UpdateKillUI();

        if (currentKills >= killsNeeded)
        {
            WinLevel();
        }
    }

    // Updates the big white number (e.g., 0, 1, 2...)
    void UpdateKillUI()
    {
        if (killNumberText != null)
        {
            killNumberText.text = currentKills.ToString();
        }
    }

    // Updates the goal text to match your Inspector settings (e.g., "/ 20 KILLS")
    void UpdateGoalUI()
    {
        if (goalNumberText != null)
        {
            goalNumberText.text = "/ " + killsNeeded.ToString();
        }
    }

    public void PlayerDied()
    {
        if (isGameOver) return;
        
        isGameOver = true;
        Debug.Log("Starting Death Transition...");
        StartCoroutine(DeathTransition());
    }

    System.Collections.IEnumerator DeathTransition()
    {
        yield return new WaitForSecondsRealtime(3f); 
        SceneManager.LoadScene("StartScene");
    }

    void WinLevel()
    {
        Debug.Log("LEVEL COMPLETE! Returning to Map Selection...");
        isGameOver = true;
        Invoke("LoadMapScene", 3f);
    }

    void LoadMapScene()
    {
        SceneManager.LoadScene("Map Scene");
    }
}