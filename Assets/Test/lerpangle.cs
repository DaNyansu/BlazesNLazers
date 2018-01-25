using UnityEngine;
using System.Collections;

public class lerpangle : MonoBehaviour
{
    public float minAngle = 0.0F;
    public float maxAngle = 90.0F;
    void Update()
    {
        float angle = Mathf.LerpAngle(minAngle, maxAngle, Time.time);
        transform.eulerAngles = new Vector3(0, angle, 0);
    }

}
