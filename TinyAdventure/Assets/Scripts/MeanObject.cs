using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeanObject : MonoBehaviour
{
    public GameObject objectA;
    public GameObject objectB;

    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 aPosition = objectA.transform.position;
        Vector3 bPosition = objectB.transform.position;
        Vector3 meanPosition = new Vector3(
            (aPosition.x + bPosition.x) / 2,
            (aPosition.y + bPosition.y) / 2,
            (aPosition.z + bPosition.z) / 2
        );
        gameObject.transform.position = meanPosition;
    }
}
