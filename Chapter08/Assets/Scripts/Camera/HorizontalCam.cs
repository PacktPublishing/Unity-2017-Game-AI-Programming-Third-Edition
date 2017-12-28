using UnityEngine;
using System.Collections;

public class HorizontalCam : MonoBehaviour {
    [SerializeField]
    private Transform target;

    private Vector3 targetPositon;

    private void Update() {
        targetPositon = transform.position;
        targetPositon.z = target.transform.position.z;
        transform.position = Vector3.Lerp(transform.position, targetPositon, Time.deltaTime);
    }
}
