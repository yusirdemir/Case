using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Managers
    [SerializeField] private GameManager _gameManager;

    // Movement settings
    [SerializeField] private float _forwardMovementSpeed = 10f;
    [SerializeField] private float _horizontalMovementSpeed = 15f;
    [SerializeField] private float _horizontalBoundary = 2.5f;

    // Private components
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!_gameManager.IsRunning) return;

        ForwardMovement();

        if (IsMouseButtonPressed())
        {
            HorizontalMovement();
        }
    }

    private void ForwardMovement()
    {
        Vector3 forwardMovement = Vector3.forward * _forwardMovementSpeed * Time.fixedDeltaTime;
        _rigidbody.MovePosition(_rigidbody.position + forwardMovement);
    }

    private bool IsMouseButtonPressed()
    {
        return Input.GetMouseButton(0);
    }

    private void HorizontalMovement()
    {
        float inputX = Input.GetAxis("Mouse X") * _horizontalMovementSpeed * Time.fixedDeltaTime;
        Vector3 newPosition = _rigidbody.position + new Vector3(inputX, 0, 0);
        newPosition.x = Mathf.Clamp(newPosition.x, -_horizontalBoundary, _horizontalBoundary);
        _rigidbody.MovePosition(newPosition);
    }
}
