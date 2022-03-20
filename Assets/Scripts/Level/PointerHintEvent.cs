using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class PointerHintEvent : LevelEvent
{

    public GameObject pointer;
    public Vector3 position;
    public Vector3 scale;

    public float x_init = 0f;
    public float y_init = 0f;
    public float z_init = 0f;
    public float dx = 0f;
    public float dy = 0.04f;
    public float dz = 0f;
    public float radius = 0.8f;

    private GameObject pointerInstance;


    protected override void EventStart(ReadOnlyCollection<LevelEvent> previousEvents, Queue<LevelEvent> followingEvents)
    {
        pointerInstance = Object.Instantiate(pointer, position, Quaternion.identity);
        pointerInstance.transform.localScale = scale;
    }

    protected override void EventUpdate(ReadOnlyCollection<LevelEvent> previousEvents, Queue<LevelEvent> followingEvents)
    {
        NetworkedOwnership ownershipComp = GetComponent<NetworkedOwnership>();
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

    public override void OnEventDestory() {
        Destroy(pointerInstance);
    }
}
