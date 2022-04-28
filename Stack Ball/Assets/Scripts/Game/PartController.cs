using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartController : MonoBehaviour {

    public void BreakAllParts() {
        transform.SetParent(null); //At first its parent is rotator. We have to do its parent null so it doesn't rotate.
        transform.tag = "Untagged";

        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).GetComponent<Part>().Break();     //Break all the parts.
        }

        Destroy(gameObject, 1f);            //After the break, destroy parent object.
    }
}
