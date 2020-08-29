using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAnimator : MonoBehaviour
{

    [SerializeField] float speed = 1;
    float increment = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        increment += Time.deltaTime * speed;
        increment = increment % 1;
        transform.rotation = Quaternion.Euler(0, increment * 360, 0);
    }
}
