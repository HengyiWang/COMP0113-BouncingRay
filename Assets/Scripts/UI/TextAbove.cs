using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAbove : MonoBehaviour
{
    public string Text = "Hello World";

    public Vector3 offset = new Vector3(0, 0.5f, 0);
    public float scale = 0.03f;
    public float CharacterSpacing = 4f;
    public float LineSpacing = 22f;
    public float SpaceWidth = 8f;
    public Color color = new Color(110, 190, 200);  // default light blue

    public float OptionalPlayerAppearRadius = 20;
    public GameObject OptionalSphere = null;
    private Vector3 sphereCenter;

    private GameObject textInstance;
    private static string textPrefabPath = "Simple Helvetica 1";
    private GameObject HelveticaTextPrefab;
    private GameObject OptionalPlayer;


    // Start is called before the first frame update
    void Start()
    {
        if (OptionalSphere != null)
        {
            MeshCollider collider = OptionalSphere.GetComponent<MeshCollider>();
            sphereCenter = collider.bounds.center;
        }

        HelveticaTextPrefab = Resources.Load<GameObject>(textPrefabPath);
        OptionalPlayer = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (textInstance != null)
        {
            Destroy(textInstance);
        }
        
        Collider collider = getBiggestCollider();
        float maxRadius = collider == null ? 0 : collider.bounds.extents.y;
        Vector3 center = collider == null ? transform.position : collider.bounds.center;

        textInstance = Instantiate(HelveticaTextPrefab, center, transform.rotation);

        SimpleHelvetica simpleHelvetica = textInstance.GetComponent<SimpleHelvetica>();
        simpleHelvetica.Text = Text;
        simpleHelvetica.CharacterSpacing = CharacterSpacing;
        simpleHelvetica.LineSpacing = LineSpacing;
        simpleHelvetica.SpaceWidth = SpaceWidth;
        simpleHelvetica.transform.localScale = new Vector3(scale, scale, scale);
        textInstance.transform.Translate(offset);

        MeshRenderer meshRenderer = findMeshRenderer();
        if (meshRenderer != null)
        {
            simpleHelvetica.ApplyMeshRenderer(meshRenderer);
        }

        if (color != null)
        {
            simpleHelvetica.ApplyColor(color);
        }

        //if (color != null)

        simpleHelvetica.GenerateText();

        if (OptionalSphere != null)
        {
            textInstance.transform.up = sphereCenter - transform.position;
        }

        if (OptionalPlayer != null)
        {
            if (Vector3.Distance(OptionalPlayer.transform.position, transform.position) < OptionalPlayerAppearRadius)
            {
                textInstance.transform.position += OptionalPlayer.transform.up * maxRadius;
                textInstance.transform.rotation = Quaternion.LookRotation(textInstance.transform.position - OptionalPlayer.transform.position, OptionalPlayer.transform.up);
            }
            else
            {
                Destroy(textInstance);
                return;
            }
        }
    }

    Collider getBiggestCollider()
    {
        float maxHeight = 0;
        Collider maxCollider = null;

        foreach (Collider collider in GetComponents<Collider>())
        {
            if (Mathf.Abs(collider.bounds.size.y) > maxHeight)
            {
                maxCollider = collider;
                maxHeight = Mathf.Abs(collider.bounds.size.y);
            }
        }

        return maxCollider;
    }


    MeshRenderer findMeshRenderer()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer == null)
        {
            renderer = GetComponentInChildren<MeshRenderer>();
        }
        return renderer;
    }
}
