using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour {

    private Rigidbody rb;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();

        meshRenderer.material.color = ColorManager.GetCurrentColor() + new Color(0.4f, 0.4f, 0.4f, 1f);
    }

    public void Break() {
        rb.isKinematic = false;
        meshCollider.enabled = false;

        Vector3 forcePoint = transform.parent.position;
        float paretXpos = transform.parent.position.x;
        float xPos = meshRenderer.bounds.center.x;

        Vector3 subDir = (paretXpos - xPos < 0) ? Vector3.right : Vector3.left;
        Vector3 dir = (Vector3.up * 1.5f + subDir).normalized;

        float force = Random.Range(5, 15);
        float torque = Random.Range(50, 70);

        rb.AddForceAtPosition(dir * force, forcePoint, ForceMode.Impulse);
        rb.AddTorque(Vector3.left * torque);
        rb.velocity = Vector3.down;
    }
}
