using UnityEngine;
using TMPro;

public class BallScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int _score;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("scoretrigger"))
        {
            Destroy(other.gameObject);
            _score++;
            UpdateScoreText();
        }
    }
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = _score.ToString();
        }
    }
    
}
