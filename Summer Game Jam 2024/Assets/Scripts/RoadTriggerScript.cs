using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadTriggerScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Road")
            ScrollEffectScript.GetInstance().RepositionRoadSegment();
    }
}
