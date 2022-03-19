using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerHintEvent : LevelEvent
{

    private GameObject pointer;
    private Vector3 position;
    private Vector3 scale;

    private GameObject pointerInstance;

    private NetworkedOwnership ownershipComp;
    private float x_init = 0f;
    private float y_init = 0f;
    private float z_init = 0f;
    private float dx = 0f;
    private float dy = 0.04f;
    private float dz = 0f;
    private float radius = 0.8f;

    public PointerHintEvent(GameObject pointer, Vector3 position, Vector3 scale, NetworkedOwnership ownership)
    {
        this.pointer = pointer;
        this.position = position;
        this.scale = scale;
        ownershipComp = ownership;
    }

    public override void OnDestory()
    {
        Object.Destroy(pointerInstance);
    }

    protected override void EventStart()
    {
        pointerInstance = Object.Instantiate(pointer, position, Quaternion.identity);
        pointerInstance.transform.localScale = scale;
    }

    protected override void EventUpdate()
    {
        if (!ownershipComp || (ownershipComp && ownershipComp.ownership))
        {
            x_init += dx;
            y_init += dy;
            z_init += dz;
            float x = Mathf.Sin(x_init) * radius;
            float y = Mathf.Sin(y_init) * radius;
            float z = Mathf.Sin(z_init) * radius;
            pointerInstance.transform.position = position + new Vector3(x, y, z);
        }
    }

    protected override bool IsCompleted()
    {
        GameObject all_laser_gun = GameObject.Find("All_Laser_Gun");
        foreach (Transform child in all_laser_gun.transform)
        {
            if (child.GetComponent<GunGraspable>().grasped)
            {
                return true;
            }
        }
        return false;
    }
}
