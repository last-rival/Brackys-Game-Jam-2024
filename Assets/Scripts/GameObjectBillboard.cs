using UnityEngine;

public class GameObjectBillboard : MonoBehaviour
{
    public Transform target;
    public Vector3 offSet;

    void Update()
    {
        transform.LookAt(target.position + offSet);
    }
}
