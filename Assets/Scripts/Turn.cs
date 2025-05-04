using UnityEngine;
using System.Collections;

public class Turn : MonoBehaviour
{
    public bool rotated = false;
    public bool isRotating = false;

    public void Rotate180Smooth(float duration = 0.1f)
    {
        if (!isRotating)
            StartCoroutine(RotateOverTime(180f, duration));
    }

    private IEnumerator RotateOverTime(float angle, float duration)
    {
        isRotating = true;

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0f, angle, 0f);

        float elapsed = 0f;
        while (elapsed < duration)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;
        isRotating = false;
    }
}
