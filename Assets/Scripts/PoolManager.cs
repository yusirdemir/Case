using System.Collections.Generic;
using UnityEngine;
public class PoolManager : MonoBehaviour
{
    private static PoolManager Instance;

    [SerializeField]
    private bool UseParentTransforms = false;

    [SerializeField]
    private List<PoolableType> PoolableTypes = new List<PoolableType>();

    private Dictionary<string, PoolableType> TagTypeLookup;
    private Dictionary<GameObject, PoolableType> PrefabTypeLookup;
    private Dictionary<string, Queue<GameObject>> SleepingObjects;
    private Dictionary<string, HashSet<GameObject>> ActiveObjects;
    private Dictionary<string, Transform> TypeParents;

    private void Awake()
    {
        Instance = this;

        if (UseParentTransforms)
            TypeParents = new Dictionary<string, Transform>();
        TagTypeLookup = new Dictionary<string, PoolableType>();
        PrefabTypeLookup = new Dictionary<GameObject, PoolableType>();
        SleepingObjects = new Dictionary<string, Queue<GameObject>>();
        ActiveObjects = new Dictionary<string, HashSet<GameObject>>();

        foreach (PoolableType type in PoolableTypes)
        {
            if (UseParentTransforms)
            {
                GameObject SortingParent = new GameObject(type.platformData.platformName + " Parent");
                SortingParent.transform.parent = transform;
                TypeParents[type.platformData.platformName] = SortingParent.transform;
            }

            TagTypeLookup.Add(type.platformData.platformName, type);
            PrefabTypeLookup.Add(type.platformData.prefab, type);
            SleepingObjects.Add(type.platformData.platformName, new Queue<GameObject>());
            ActiveObjects.Add(type.platformData.platformName, new HashSet<GameObject>());

            for (int i = 0; i < type.Max; i++)
            {
                GameObject pooledObject = Instantiate(type.platformData.prefab, UseParentTransforms ? TypeParents[type.platformData.platformName] : transform);
                pooledObject.name = type.platformData.platformName;
                pooledObject.SetActive(false);
                SleepingObjects[type.platformData.platformName].Enqueue(pooledObject);
            }
        }
    }

    public static void Destroy(GameObject obj) => Instance._Destroy(obj);

    private void _Destroy(GameObject obj)
    {
        string sortingTag = obj.name;
        if (!ActiveObjects.ContainsKey(sortingTag)) return;
        if (!ActiveObjects[sortingTag].Contains(obj)) return;

        obj.SetActive(false);
        ActiveObjects[sortingTag].Remove(obj);
        SleepingObjects[sortingTag].Enqueue(obj);
    }

    public static GameObject Generate<T>(T Identifier, Vector3 Position, Quaternion Rotation) =>
        Instance._Generate(Identifier, Position, Rotation);

    private GameObject _Generate<T>(T Identifier, Vector3 Position, Quaternion Rotation)
    {
        string Tag = "";

        if (Identifier is string)
        {
            if (!SleepingObjects.ContainsKey(Identifier as string)) return null;
            else Tag = Identifier as string;
        }
        else if (Identifier is GameObject)
        {
            if (!PrefabTypeLookup.ContainsKey(Identifier as GameObject)) return null;
            else Tag = PrefabTypeLookup[Identifier as GameObject].platformData.platformName;
        }
        else return null;

        GameObject Member;

        if (SleepingObjects[Tag].Count == 0 && TagTypeLookup[Tag].AutoExpand)
        {
            Member = Instantiate(TagTypeLookup[Tag].platformData.prefab, UseParentTransforms ? TypeParents[Tag] : transform);
            Member.name = Tag;
        }
        else if (SleepingObjects[Tag].Count > 0)
            Member = SleepingObjects[Tag].Dequeue();
        else return null;

        Member.transform.position = Position;
        Member.transform.rotation = Rotation;
        ActiveObjects[Tag].Add(Member);
        Member.SetActive(true);

        return Member;
    }
}
