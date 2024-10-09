using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] private GenerateType _generateType = GenerateType.Progressive;
    [SerializeField, Tooltip("Creates platforms according to the order specified here.")]
    private List<PlatformData> _platforms = new List<PlatformData>();
    [SerializeField] private List<Platform> _activePlatforms = new List<Platform>();

    private void Start()
    {
        GeneratePlatforms(15);
    }

    public void GeneratePlatforms(int count)
    {
        for (int i = 0; i < count; i++)
        {
            PlatformData platformData = GetNextPlatformData();

            Vector3 position = _activePlatforms.Count > 0 ? _activePlatforms[^1].EndTransform.position : Vector3.zero;

            GameObject platform = PoolManager.Generate(platformData.prefab.name, position, Quaternion.identity);
            _activePlatforms.Add(platform.GetComponent<Platform>());
        }
    }

    private PlatformData GetNextPlatformData()
    {
        if (_generateType == GenerateType.Progressive)
        {
            return GetProgressivePlatformData();
        }
        else
        {
            return GetRandomPlatformData();
        }
    }

    private PlatformData GetProgressivePlatformData()
    {
        if (_activePlatforms.Count > 0)
        {
            int currentIndex = _platforms.FindIndex(p => p.platformName == _activePlatforms[^1].PlatformName);
            int nextIndex = (currentIndex + 1) % _platforms.Count;
            return _platforms[nextIndex];
        }
        return _platforms[0];
    }

    private PlatformData GetRandomPlatformData()
    {
        int randomIndex = Random.Range(0, _platforms.Count);
        return _platforms[randomIndex];
    }

    public void DestroyPlatforms(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (_activePlatforms.Count <= 0) return;

            PoolManager.Destroy(_activePlatforms[0].gameObject);
            _activePlatforms.RemoveAt(0);
        }
    }

    public void TriggerPlatform()
    {
        DestroyPlatforms(1);
        GeneratePlatforms(1);
    }
}
