using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RadialSet : MonoBehaviour {

    public float minangle;
    public float maxangle;

    private float angle;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        angle = maxangle - minangle;

        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -minangle));

        GetComponent<Image>().fillAmount = angle/360f;

        

    }

    void SetAngle()
    {
        angle = maxangle - minangle;

        GetComponent<Image>().fillAmount = angle / 360f;
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, minangle));
    }
}
