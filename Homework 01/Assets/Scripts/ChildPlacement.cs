using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChildPlacement : MonoBehaviour
{

    //Purpose of this script is to find all of the children of the object with the tag "Obstacles" and arrange the children in a circle around the center with a
    // radius of "distFromCenter". Rather do this since we could make the game easier or harder in the future by varying the number of objects you need to avoid
   
    // Start is called before the first frame update
    void Start()
    {
        float distFromCenter = 15;

        List<GameObject> childObjects = new System.Collections.Generic.List<GameObject>();

        foreach(Transform child in transform)
        {
            if(child.gameObject.CompareTag("Obstacle")) childObjects.Add(child.gameObject);
        }

        int numOfChildren = childObjects.Count;
        float angleBetweenChildren = 360/numOfChildren;
        angleBetweenChildren = UnityEngine.Mathf.Deg2Rad * angleBetweenChildren;

        for (int i = 0; i < numOfChildren; i++)
        {
            childObjects[i].transform.position = new Vector3(UnityEngine.Mathf.Sin(angleBetweenChildren*i), UnityEngine.Mathf.Cos(angleBetweenChildren*i), 0) * distFromCenter;
        }
    }

    
}
