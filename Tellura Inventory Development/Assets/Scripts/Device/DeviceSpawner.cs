using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceSpawner : MonoBehaviour {
    float size = 0;
    float speed = 5;
    float completeAt = 0.999f;
    public bool scaleX = true;
    public bool scaleY = true;
	// Use this for initialization
	void Start () {
        UpdateScale();
    }
	
	// Update is called once per frame
	void Update () {
        if (size > completeAt) {
            transform.localScale = Vector3.one;
            Destroy(this);
            return;
        }
        size = Mathf.Lerp(size, 1, Time.deltaTime * speed);
        UpdateScale();
	}
    void UpdateScale() {
        transform.localScale = new Vector3(
            scaleX ? size : 1,
            scaleY ? size : 1,
            1);

    }
}
