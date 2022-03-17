using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsGeneration : MonoBehaviour
{

    public GameObject sphere;
    public GemCatalogue gemCatalogue;
    public int amount = 100;
    public int randomSeed = 42;

    private Vector3 sphereCenter;
    private float sphereRadius;
    private List<GameObject> existingDecorations;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(randomSeed);

        MeshCollider collider = sphere.GetComponent<MeshCollider>();
        sphereCenter = collider.bounds.center;
        sphereRadius = collider.bounds.extents.x;

        existingDecorations = new List<GameObject>();

        for (int i = 0; i < amount; i++)
        {
            GameObject chosenPrefab = gemCatalogue.GetPrefab(Random.Range(0, gemCatalogue.size));
            Vector3 chosenLocation = Random.onUnitSphere * sphereRadius + sphereCenter;
            GameObject spawnedInstance = Instantiate(chosenPrefab, chosenLocation, Quaternion.identity);
            spawnedInstance.transform.up = (sphereCenter - chosenLocation).normalized;
            existingDecorations.Add(spawnedInstance);
        }

    }

    // Update is called once per frame
    void Update()
    {
        // empty
    }
}
