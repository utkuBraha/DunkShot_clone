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
            if (scoreTriggerController != null)
            {
                scoreTriggerController.DeactivateRotation(gameObject);
            }

            scoreTriggerController = other.gameObject.GetComponent<ScoreTriggerController>();
            scoreTriggerController.ActivateRotation(gameObject);
            other.gameObject.SetActive(false);
            _score++;
            UpdateScoreText();
            consecutiveCollisions++;
            

            if (consecutiveCollisions >= 2)
            {
                ActivateGameObject();
                _score ++;
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
            gameManager.GameOver();
        }
        else if (other.gameObject.CompareTag("hoopboundary"))
        {
            consecutiveCollisions = 0;
            DeactivateGameObject();
        }
        else if (other.gameObject.CompareTag("triggerexit"))
        {
            Debug.Log("Trigger exit"+ other.gameObject.name);
            scoreTriggerController.ActivateRotation(gameObject);
            other.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
       
        if (_score > 0)
        {
            GameObject firstHoop = GameObject.FindWithTag("firsthoop");

            if (firstHoop != null)
            {
                Destroy(firstHoop);
            }
        }
        if (_score > 1)
        {
            GameObject secondHoop = GameObject.FindWithTag("secondhoop");

            if (secondHoop != null)
            {
                Destroy(secondHoop);
            }
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
