using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Player player;
    private UIManager uiManager;
    private bool isGameOver;

    public static event System.Action OnGameOver = delegate { };
    public static float score;
    public static float doubleClickMaxDelayInSeconds = .25f;

    private void Awake()
    {
        OnGameOver = delegate { };
        player = FindObjectOfType<Player>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        ResumeTheGame();
    }

    private void Update()
    {
        if (!isGameOver)
        {
            score += Time.deltaTime;
            if (SpawnManager.player.hunger < 0f)
            {
                isGameOver = true;
                OnGameOver();
            }
        }
    }

    public void PauseTheGame()
    {
        Time.fixedDeltaTime = 0;
        Time.timeScale = 0;
        uiManager.ShowPauseMenu();
    }

    public void ResumeTheGame()
    {
        uiManager.HidePauseMenu();
        Time.timeScale = 1;
        Time.fixedDeltaTime = Time.timeScale / 50f;
    }

    public static void ModifyScore(float value)
    {
        score += Mathf.Abs(value);
    }
}