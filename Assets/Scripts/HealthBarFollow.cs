using UnityEngine;

public class HealthBarFollow : MonoBehaviour
{
    public Transform target;           
    Quaternion fixedRotation;  

    void Start()
    {
        fixedRotation = Quaternion.identity; 
    }

    void LateUpdate()
    {
        if (target != null)
        {
            transform.rotation = fixedRotation;
        }
    }
}
