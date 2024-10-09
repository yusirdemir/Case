using UnityEngine;

public class CoinManager : MonoBehaviour
{
    private const string CoinKey = "Coin";

    public int Coin
    {
        get => PlayerPrefs.GetInt(CoinKey);
        set => PlayerPrefs.SetInt(CoinKey, value);
    }
}
