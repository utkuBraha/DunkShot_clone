using UnityEngine;
using TMPro;

public class BallScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    public GameManager gameManager;
    private int _score;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("scoretrigger"))
        {
            Destroy(other.gameObject);
            _score++;
            UpdateScoreText();
        }else if (other.gameObject.CompareTag("gameovertrigger"))
        {
            Debug.Log("Game Over");
            gameManager.GameOver();
        }
    }
    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = _score.ToString();
        }
    }

    public int GetScore()
    {
        return _score;
    }
}
