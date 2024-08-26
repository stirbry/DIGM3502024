using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampNotif : MonoBehaviour
{
    public float rattleAmount = 0.1f, rattleSpeed = 0.2f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    IEnumerator Rattle()
    {
        while (true)
        {
            // Generate random offsets in the x axis
            float offsetX = Random.Range(-rattleAmount, rattleAmount);

            // apply offset to x only
            transform.position = initialPosition + new Vector3(offsetX, 0, 0);

            yield return new WaitForSeconds(1f / rattleSpeed);
        }
    }

    public void StartRattle()
    {
        StopAllCoroutines();
        StartCoroutine(Rattle());
    }

    public void StopRattle()
    {
        StopAllCoroutines(); // Stop rattling
        transform.position = initialPosition;
    }
}
