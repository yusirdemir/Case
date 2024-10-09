using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    // Managers
    [SerializeField] private PlatformManager _platformManager;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private CoinManager _coinManager;
    [SerializeField] private PlayerAnimator _playerAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            HandleCoinCollision(other);
        }
        
        if (other.CompareTag("Obstacle"))
        {
            HandleObstacleCollision(other);
        }

        if (other.CompareTag("Gate"))
        {
            HandleGateCollision(other);
        }
    }

    private void HandleCoinCollision(Collider coin)
    {
        coin.gameObject.SetActive(false);
        _coinManager.Coin += 1;
        _uiManager.UpdateCoinText();
    }

    private void HandleObstacleCollision(Collider obstacle)
    {
        _playerAnimator.Die();
        _uiManager.FailGame();
    }

    private void HandleGateCollision(Collider gate)
    {
        Invoke(nameof(TriggerPlatform), 2f);
    }

    private void TriggerPlatform()
    {
        _platformManager.TriggerPlatform();
    }
}
