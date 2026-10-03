using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private Rigidbody rb;

    public float minS;
    public float maxS;

    private Vector3 prev = new Vector3(-1, -1, -1);
    private Vector3 RandomVector(float min, float max)
    {
        var x = Random.Range(min, max);
        var z = Random.Range(min, max);
        return new Vector3(x, 0, z);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Fling()
    {
        var rb = GetComponent<Rigidbody>();
        var curr = RandomVector(minS, maxS);
        if ((prev.x > 0 && curr.x > 0) || (prev.x < 0 && curr.x < 0))
        {
            curr.x = curr.x * -1;
        }
        if ((prev.z > 0 && curr.z > 0) || (prev.z < 0 && curr.z < 0))
        {
            curr.z = curr.z * -1;
        }
        rb.velocity = curr;
        prev = curr;

    }
    //// Update is called once per frame
    //void Update()
    //{

    //}


}