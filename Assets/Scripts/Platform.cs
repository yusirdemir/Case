using UnityEngine;

public class Platform : MonoBehaviour
{
    // Private fields
    [SerializeField] private PlatformData _platformData;
    [SerializeField] private Transform _endTransform;
    [SerializeField] private GameObject _collectableParent;

    public string PlatformName => _platformData.platformName;
    public Transform EndTransform => _endTransform;

    private void OnEnable()
    {
        ActivateCollectables();
    }

    private void ActivateCollectables()
    {
        foreach (Transform child in _collectableParent.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

}
