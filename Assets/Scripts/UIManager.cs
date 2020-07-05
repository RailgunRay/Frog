using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    #pragma warning disable 0649
    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject gameOverScreen;

    [SerializeField]
    private GameObject uiElements;

    [SerializeField]
    private Slider hungerScale;

    [SerializeField]
    private Score gameScore;

    [SerializeField]
    private TextMeshProUGUI finalScoreText;
    #pragma warning restore 0649

    private void Start()
    {
        gameScore.SetScoreText(0);
        gameOverScreen.SetActive(false);
        uiElements.SetActive(true);
        GameManager.OnGameOver += ActivateGameOverScreen;
    }

    private void Update()
    {
        gameScore.SetScoreText(Mathf.FloorToInt(GameManager.score));
        hungerScale.value = SpawnManager.player.hunger / 100f;
    }

    public void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    public void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
    }

    private void ActivateGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        uiElements.SetActive(false);
        finalScoreText.text = "Final Score: " + Mathf.FloorToInt(GameManager.score);
    }
}
