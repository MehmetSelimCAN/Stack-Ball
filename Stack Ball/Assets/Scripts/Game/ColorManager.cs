using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour {

    private static Color currentColor;
    private static Transform pole;

    private void Awake() {
        currentColor = Random.ColorHSV(0f, 1f, 0.5f, 0.5f, 0.55f, 1f);
        pole = GameObject.Find("Pole").transform;
        pole.GetComponent<MeshRenderer>().material.color = currentColor + new Color(0.4f, 0.4f, 0.4f, 1f);
    }

    public static Color GetCurrentColor() {
        return currentColor;
    }

}
