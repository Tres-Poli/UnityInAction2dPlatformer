using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform Target;
    public float SmoothTime = 0.2f;

    private Vector3 _velocity = Vector3.zero;

    void LateUpdate()
    {
        Vector3 targetTransform = new Vector3(Target.position.x, Target.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetTransform, ref _velocity, SmoothTime);
    }
}
