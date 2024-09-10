using UnityEngine;

public class ObjectTransformRotator : MonoBehaviour
{
    [SerializeField] private Vector3 rotation;
    [SerializeField] private float speed;
    [SerializeField] private bool randomisePolarity;

    private void Start()
    {
        if (randomisePolarity && Random.value > 0.5f)
            rotation *= -1f;
    }

    private void Update()
    {
        transform.Rotate(rotation * speed * Time.deltaTime);
        //transform.rotation = Quaternion.Euler(
        //    Vector3.Lerp(
        //        transform.rotation.eulerAngles, 
        //        transform.rotation.eulerAngles + rotation,
        //        speed * Time.fixedDeltaTime
        //    ));
    }
}
