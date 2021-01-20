using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun_Rotation : MonoBehaviour
{
    [SerializeField]
    public int sun_position;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Rotate(Vector3.up, 60, 1.0f));
            sun_position += 1;
            if (sun_position > 5)
            {
                sun_position = 0;
            }
        }
    }

    public Sun_Rotation()
    {
        sun_position = 0;
    }

    public void Next_Sun_Position()
    {
        StartCoroutine(Rotate(Vector3.up, 60, 1.0f));
        sun_position += 1;
        if (sun_position > 5)
        {
            sun_position = 0;
        }
    }



    IEnumerator Rotate(Vector3 axis, float angle, float duration = 1.0f)
    {
        Quaternion from = transform.rotation;
        Quaternion to = transform.rotation;
        to *= Quaternion.Euler(axis * angle);

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.rotation = to;

        StopAllCoroutines();
    }
}
