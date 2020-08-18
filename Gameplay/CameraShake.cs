using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake CamShake;
    public GameObject Camera;

    void Start()
    {
        CamShake = this;
    }
    public IEnumerator Camera_Shake()
    {
        Vector3 original_pos = Camera.transform.localPosition;

        float elapsed = 0f;

        while(elapsed < .5f)
        {
            float z = Random.Range(-1f, 1f) * .4f;

            Camera.transform.eulerAngles = new Vector3(0, 0, z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Camera.transform.localPosition = original_pos;
    }
}
