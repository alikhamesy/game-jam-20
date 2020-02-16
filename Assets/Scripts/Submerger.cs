using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submerger : MonoBehaviour
{
    // depth is between 0 and 1
    public float targetDepth = 1.0f;
    public float submergeTime = 0.5f;
    private float currentDepth = 1.0f;
    private float initialSize;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        initialSize = transform.localScale.x;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if (targetDepth < currentDepth)
        {
            // reduce depth
            currentDepth -= Mathf.Min(Time.deltaTime / submergeTime, currentDepth - targetDepth);

            // sink
            transform.Translate(Vector3.up * Time.deltaTime / submergeTime * (targetDepth - currentDepth));

            // shrink by 20% of target depth
            float minSize = ((4 + targetDepth) / 5) * initialSize;
            float size = Mathf.Max(minSize, transform.localScale.x * (1 - Time.deltaTime / submergeTime));
            transform.localScale = new Vector3(size, size, size);
        }
        else if (currentDepth < targetDepth)
        {
            // increase depth
            currentDepth += Mathf.Min(Time.deltaTime / submergeTime, targetDepth - currentDepth);

            // rise
            transform.Translate(Vector3.down * Time.deltaTime / submergeTime * (currentDepth - targetDepth));

            // grow to regular size
            float maxSize = targetDepth * initialSize;
            float size = Mathf.Min(maxSize, transform.localScale.x * (1 + Time.deltaTime / submergeTime));
            transform.localScale = new Vector3(size, size, size);
        }

        spriteRenderer.color = new Color(currentDepth, currentDepth, currentDepth, currentDepth);

    }
}
