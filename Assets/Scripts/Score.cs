using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Score : MonoBehaviour
{
    private Text _label;

    private void Awake()
    {
        _label = GetComponent<Text>();
    }

    private void OnEnable()
    {
        ScoreSystem.OnScoreUpdated += UpdateScoreText;
    }

    private void OnDisable()
    {
        ScoreSystem.OnScoreUpdated -= UpdateScoreText;
    }

    private void UpdateScoreText(int score)
    {
        _label.text = "SCORE: " + score.ToString();
    }
}
