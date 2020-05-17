using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public float MinDistance = 1.0f;
    public float MaxDistance = 10.0f;
    public float smooth = 10.0f;
    Vector3 DollyDir;
    public Vector3 DollyDirAdjusted;
    public float Distance;

    private void Awake()
    {
        DollyDir = transform.localPosition.normalized;
        Distance = transform.localPosition.magnitude;
    }

    private void Update()
    {
        Vector3 DesiredCameraPosition = transform.parent.TransformPoint(DollyDir * MaxDistance);
        RaycastHit Hit;

        if (Physics.Linecast(transform.parent.position,DesiredCameraPosition,out Hit))
        {
            Distance = Mathf.Clamp(Hit.distance*0.9f, MinDistance, MaxDistance);
        }
        else
        {
            Distance = MaxDistance;
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, DollyDir * Distance, Time.deltaTime * smooth);
    }
}
