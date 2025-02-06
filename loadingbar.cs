using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadingbar : MonoBehaviour
{
    private Vector3 startPosition = new Vector3(57.13f, 1.67f, -2);
    private Vector3 endPosition = new Vector3(60.79f, 1.67f, -2);
    private float speed = 2f;
    private bool movingToEnd = true;
    private SpriteRenderer spriteRenderer;
    private float fadeSpeed = 1f;
    private bool fadingOut = false;
    private bool fadingIn = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = startPosition;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (movingToEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
            if (transform.position == endPosition)
            {
                movingToEnd = false;
                fadingOut = true;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
            if (transform.position == startPosition)
            {
                movingToEnd = true;
                fadingIn = true;
            }
        }

        if (fadingOut)
        {
            Color color = spriteRenderer.color;
            color.a -= fadeSpeed * Time.deltaTime;
            if (color.a <= 0)
            {
                color.a = 0;
                fadingOut = false;
                transform.position = startPosition;
                fadingIn = true;
            }
            spriteRenderer.color = color;
        }

        if (fadingIn)
        {
            Color color = spriteRenderer.color;
            color.a += fadeSpeed * Time.deltaTime;
            if (color.a >= 1)
            {
                color.a = 1;
                fadingIn = false;
            }
            spriteRenderer.color = color;
        }
    }
}