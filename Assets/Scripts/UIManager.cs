using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Managers
    [SerializeField] private CoinManager _coinManager;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private PlayerAnimator _playerAnimator;

    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _failPanel;
    [SerializeField] private TextMeshProUGUI _coinText;

    private void Start()
    {
        InitializeUI();
    }

    private void InitializeUI()
    {
        _startPanel.SetActive(true);
        _failPanel.SetActive(false);
        UpdateCoinText();
    }

    public void StartGame()
    {
        _startPanel.SetActive(false);
        _gameManager.StartGame();
        _playerAnimator.Run();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void FailGame()
    {
        _failPanel.SetActive(true);
        _gameManager.StopGame();
    }

    public void UpdateCoinText()
    {
        _coinText.text = _coinManager.Coin.ToString();
    }
}
