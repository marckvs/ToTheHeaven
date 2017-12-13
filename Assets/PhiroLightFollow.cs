using UnityEngine;

public class PhiroLightFollow : MonoBehaviour {
    Transform Phiro;
    Vector3 newPosition;
    Light lightComponent;
    [Range(0, -2.0f)]
    public float offset = -0.35f;
    [Range(0, 20.0f)]
    public float lightIntensity = 6.5f;
    [Range(0, 30.0f)]
    public float lightRange = 10.0f;

	// Use this for initialization
	void Awake () {
        Phiro = GameObject.FindGameObjectWithTag("Phiro").transform;
        lightComponent = this.GetComponent<Light>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
        lightComponent.intensity = lightIntensity;
        lightComponent.range = lightRange;
        newPosition = new Vector3(Phiro.position.x, Phiro.position.y, offset);
        transform.position = newPosition;


	}
}
