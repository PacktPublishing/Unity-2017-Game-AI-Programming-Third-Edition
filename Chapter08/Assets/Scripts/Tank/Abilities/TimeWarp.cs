using System.Collections;
using UnityEngine;

public class TimeWarp : MonoBehaviour {

    private bool canUse = true;
    private float abilityTimer = 0.0f;
    private float abilityDuration = 3.5f;
    

    private void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            if (canUse) {
                StartCoroutine(EnableTimeWarp());
            }
        }
    }

    private IEnumerator EnableTimeWarp() {
        canUse = false;
        Time.timeScale = 0.5f;
        while (abilityTimer < abilityDuration) {
            abilityTimer += Time.deltaTime;
            yield return null;
        }
        Time.timeScale = 1;
        abilityTimer = 0.0f;
        canUse = true;
    }
}
