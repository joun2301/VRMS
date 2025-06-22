using UnityEngine;

public class MoveSky : MonoBehaviour
{
    float degree;

	void Start ()
    {
        degree = 0;
    }
	
	void Update ()
    {
        degree += Time.deltaTime / 2;
        if (degree >= 360)
            degree = 0;

        RenderSettings.skybox.SetFloat("_Rotation", degree);
    }
}
