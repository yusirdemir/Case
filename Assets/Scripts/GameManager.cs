using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool IsRunning { get; private set; } = false;

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    public void StartGame()
    {
        IsRunning = true;
    }

    public void StopGame()
    {
        IsRunning = false;
    }
}
