using UnityEngine;

using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;
    
    private bool _isGameActive;

  

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        Destroy(GameObject.FindWithTag("Player"));
    }
    public void StartGame()
    {
        
        _isGameActive = true;
        panel.SetActive(false);
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
