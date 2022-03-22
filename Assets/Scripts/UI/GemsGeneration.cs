using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemsGeneration : MonoBehaviour
{

    public GameObject sphere;
    public GemCatalogue gemCatalogue;
    public int amount = 100;
    public int randomSeed = 42;
    public float scale = 0.5f;

    private Vector3 sphereCenter;
    private float sphereRadius;
    private List<GameObject> existingDecorations;
    private Material sphereMaterial;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(randomSeed);

        MeshCollider collider = sphere.GetComponent<MeshCollider>();
        sphereCenter = collider.bounds.center;
        sphereRadius = collider.bounds.extents.x;
        sphereMaterial = sphere.GetComponent<MeshRenderer>().material;

        existingDecorations = new List<GameObject>();
        GameObject gemsContainer = new GameObject("GemsContainer");

        for (int i = 0; i < amount; i++)
        {
            GameObject chosenPrefab = gemCatalogue.GetPrefab(Random.Range(0, gemCatalogue.size));
            Vector3 chosenLocation = Random.onUnitSphere * sphereRadius + sphereCenter;
            GameObject spawnedInstance = Instantiate(chosenPrefab, chosenLocation, Quaternion.identity);
            spawnedInstance.transform.localScale = new Vector3(scale, scale, scale);
            spawnedInstance.transform.parent = gemsContainer.transform;
            setMaterial(spawnedInstance, sphereMaterial);
            spawnedInstance.transform.up = (sphereCenter - chosenLocation).normalized;
            existingDecorations.Add(spawnedInstance);
        }

    }

    void setMaterial(GameObject gameobject, Material material)
    {
        foreach (MeshRenderer renderer in gameobject.GetComponents<MeshRenderer>())
        {
            renderer.material = material;
        }

        foreach (MeshRenderer renderer in gameobject.GetComponentsInChildren<MeshRenderer>())
        {
            renderer.material = material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // empty
    }
}
