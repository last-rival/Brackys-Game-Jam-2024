using UnityEngine;

public class GameObjectBillboard : MonoBehaviour
{
    public Transform target;
    public Vector3 offSet;
    public float yOffSet;

    void Update()
    {
        transform.LookAt(target.position + offSet);
        var euler = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(euler.x, euler.y + yOffSet, euler.z);
    }
}
