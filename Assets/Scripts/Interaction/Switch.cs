using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool initActivated;
    private Material m;
    private GameObject _light;
    private Color eColor;

    public GameObject gem;
    // Start is called before the first frame update
    void Start()
    {
        this.m = GetComponent<Renderer>().material;
        this.eColor = this.m.GetColor("_EmissionColor");
        _light = transform.GetChild(0).gameObject;
        if (initActivated)
        {
            activate();
        }
        else
        {
            deactivate();
        }
    }

    public void activate()
    {
        _light.SetActive(true);
        if (gem)
        {
            gem.SetActive(true);
        }
        m.SetColor("_EmissionColor", this.eColor);
    }

    public void deactivate()
    {
        _light.SetActive(false);
        if (gem)
        {
            gem.SetActive(false);
        }
        m.SetColor("_EmissionColor", 0 * this.eColor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
