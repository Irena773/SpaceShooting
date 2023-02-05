using UnityEngine;

public class ShipRotation : MonoBehaviour
{
    private Vector3 TARGET = new Vector3(0.0f, 0.0f, 0.0f);
    private float angle = 20.0f;

    void Update()
    {
        //RotateAround(’†S‚ÌêŠ,²,‰ñ“]Šp“x)
        transform.RotateAround(TARGET, Vector3.up, angle * Time.deltaTime);

    }
}

