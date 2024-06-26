using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int ScoreCount;
    private TextMeshProUGUI text;
    private EventBus eventBus;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();

        eventBus = ServiceLocator.Instance.Get<EventBus>();
        eventBus.Subscribe<ScoreChanged>(UpdateScore);
        eventBus.Subscribe<ScoreMinus>(ScoreMinus);

        ScoreCount = PlayerPrefs.GetInt("Score");
        text.text = ScoreCount.ToString();
    }

    private void OnDestroy()
    {
        eventBus.Unsubscribe<ScoreChanged>(UpdateScore);
        eventBus.Unsubscribe<ScoreMinus>(ScoreMinus);

        PlayerPrefs.Save();
    }

    public void UpdateScore(ScoreChanged score)
    {
        ScoreCount += score.Score;
        ScoreCount = Mathf.Clamp(ScoreCount, 0, int.MaxValue);
        text.text = ScoreCount.ToString();

        PlayerPrefs.SetInt("Score", ScoreCount);
        PlayerPrefs.Save();
    }

    public void ScoreMinus(ScoreMinus score)
    {
        int scoreCurrent = PlayerPrefs.GetInt("Score");

        ScoreCount = scoreCurrent - (int)(scoreCurrent * 0.3);
        text.text = ScoreCount.ToString();

        PlayerPrefs.SetInt("Score", ScoreCount);
        PlayerPrefs.Save();
    }
}
