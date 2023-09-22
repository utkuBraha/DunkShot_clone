
using UnityEngine;
using TMPro;

public class BallScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText; 
    public GameManager gameManager;
    public GameObject gameObjToActivate;
    private int _score;
    private int consecutiveCollisions = 0;
    public ScoreTriggerController scoreTriggerController;
    private int _highScore;
    private string highScoreKey = "HighScore"; 

    private void Start()
    {
        _highScore = PlayerPrefs.GetInt(highScoreKey, 0);

        UpdateScoreText();
        UpdateHighScoreText();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("scoretrigger"))
        {
            Debug.Log("Score");
            if (scoreTriggerController != null)
            {
                Debug.Log("Deactivate Rotation");
                scoreTriggerController.DeactivateRotation();
            }

            scoreTriggerController = other.gameObject.GetComponent<ScoreTriggerController>();
            scoreTriggerController.ActivateRotation();
            other.gameObject.SetActive(false);
            _score++;
            UpdateScoreText();
            consecutiveCollisions++;
            if (consecutiveCollisions == 2)
            {
                ActivateGameObject();
            }
            if (_score > _highScore)
            {
                _highScore = _score;
                PlayerPrefs.SetInt(highScoreKey, _highScore);
                PlayerPrefs.Save();
                UpdateHighScoreText();
            }
        }
        else if (other.gameObject.CompareTag("gameovertrigger"))
        {
            Debug.Log("Game Over");
            gameManager.GameOver();
        }
        else if (other.gameObject.CompareTag("hoopboundary"))
        {
            consecutiveCollisions = 0;
            DeactivateGameObject();
        }
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = _score.ToString();
        }
    }

    private void UpdateHighScoreText()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "HIGH SCORE: " + _highScore.ToString();
        }
    }

    public int GetScore()
    {
        return _score;
    }

    private void ActivateGameObject()
    {
        if (gameObjToActivate != null)
        {
            gameObjToActivate.SetActive(true);
        }
    }

    private void DeactivateGameObject()
    {
        if (gameObjToActivate != null)
        {
            gameObjToActivate.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt(highScoreKey, _highScore);
        PlayerPrefs.Save();
    }
}
