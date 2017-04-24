using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceSpawner : MonoBehaviour {
    BoxCollider bc;
    float size = 0.001f;
    float speed = 5;
    float completeAt = 0.999f;
    public bool scaleX = true;
    public bool scaleY = true;

    private void Awake() {
        bc = gameObject.GetComponent<BoxCollider>();
    }

	void Start () {
        UpdateScale();
    }
	
	void Update () {
        if (size > completeAt) {
            size = 1;
            UpdateScale();
            Destroy(this);
            return;
        }
        size = Mathf.Lerp(size, 1, Time.deltaTime * speed);
        UpdateScale();
	}
    void UpdateScale() {
        float reciprocal = 1/size;
        if (bc != null) {
            bc.size =  new Vector3(
                scaleX ? reciprocal : 1,
                scaleY ? reciprocal : 1,
                0.001f);
        }
        transform.localScale = new Vector3(
            scaleX ? size : 1,
            scaleY ? size : 1,
            1);

    }
}
