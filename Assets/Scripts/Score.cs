using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Score : MonoBehaviour
{
    TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    public void SetScoreText(int score)
    {
        scoreText.text = "Score: " + score;
    }
}
