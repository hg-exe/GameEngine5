using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isActive = false;

    [SerializeField] Vector3 endPosition;
    Vector3 startPosition;

    float lerpValue;

    public float speed = 20.0f;
    float lerpSpeed;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        float distance = Vector3.Distance(startPosition, endPosition);

        lerpSpeed = speed / distance;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, lerpValue);
            lerpValue += Time.deltaTime * lerpSpeed;
            if(lerpValue > 1.0f)
            {
                lerpValue = 1.0f;
                isActive = false;   
            }
        }
        //print(lerpValue);
    }
}
