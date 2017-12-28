using System.Collections;
using UnityEngine;

public class HulkMode : MonoBehaviour {

    private bool canUse = true;
    private Transform tankTransform;
    private Vector3 originalSize;
    private float abilityTimer = 0.0f;
    private float abilityDuration = 3.5f;

    private void Awake() {
        if(tankTransform == null) {
            tankTransform = transform;
            originalSize = transform.localScale;
        }   
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.H)) {
            if (canUse) {
                StartCoroutine(EnableHulkMode());
            }
        }
    }

    private IEnumerator EnableHulkMode() {
        canUse = false;
        tankTransform.localScale = Vector3.one * 4;
        while(abilityTimer < abilityDuration) {
            abilityTimer+= Time.deltaTime;
            yield return null;
        }
        abilityTimer = 0.0f;
        canUse = true;
        tankTransform.localScale = originalSize;
    }
}
