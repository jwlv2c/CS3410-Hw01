using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChildPlacement : MonoBehaviour
{
    /*
        Author:
            Jacob Listhartke
            
        Script Purpose:
            Finds all of the children of the object that this script is attached to 
            with the "Obstacle" Tag and arranges the children around itself in
            an equally angle circle, with the radius dictated by the float at
            the top of the script "distFromCenter"

        Commentary:
            If in the future, we wanted to make the game harder, we could increase/
            decrease the number of objects, and this would assist in making that 
            difficulty much easier to place objects. 
    */

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
        angleBetweenChildren = UnityEngine.Mathf.Deg2Rad * angleBetweenChildren; //Unity Angles are in Radians, not degrees

        for (int i = 0; i < numOfChildren; i++)
        {
            childObjects[i].transform.position = new Vector3(UnityEngine.Mathf.Sin(angleBetweenChildren*i), UnityEngine.Mathf.Cos(angleBetweenChildren*i), 0) * distFromCenter;
        }
    }

    
}
