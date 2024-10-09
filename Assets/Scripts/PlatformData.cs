using UnityEngine;

[CreateAssetMenu(fileName = "NewPlatform", menuName = "Platforms/New Platform", order = 1)]
public class PlatformData : ScriptableObject
{
    public string platformName;
    public GameObject prefab;
}
