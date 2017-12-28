using UnityEngine;

public class Touch : Sense
{
    void OnTriggerEnter(Collider other)
    {
        Aspect aspect = other.GetComponent<Aspect>();
        if (aspect != null)
        {
            //Check the aspect
            if (aspect.aspectType != aspectName)
            {
                print("Enemy Touch Detected");
            }
        }
    }
}
