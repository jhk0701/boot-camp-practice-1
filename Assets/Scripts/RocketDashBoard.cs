using UnityEngine;
using TMPro;

public class RocketDashBoard : MonoBehaviour
{
    // 기능 2. 점수 표시 => RocketDashBoard로 이전
    float score;
    [Header("Score")]
    [SerializeField] private TMP_Text currentScoreTxt;
    [SerializeField] private TMP_Text highScoreTxt;

    void Start()
    {
        highScoreTxt.text = $"HIGH : {ScoreManager.instance.HighScore : 0.##} M";
    }

    
    void Update()
    {
        score = transform.position.y;
        currentScoreTxt.text = $"{score:0.##} M";

        if (ScoreManager.instance.HighScore < score)   
        {
            highScoreTxt.text = $"HIGH : {score:0.##} M";
            ScoreManager.instance.UpdateHighScore(score);
        }
    }
}