using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Gem Catalogue")]
public class GemCatalogue : ScriptableObject
{
    public List<GameObject> gemPrefabs;

    public int size { get { return gemPrefabs.Count; } }
    public GameObject GetPrefab(int i)
    {
        return gemPrefabs[i];
    }

    
}
