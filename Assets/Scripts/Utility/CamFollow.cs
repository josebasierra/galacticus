using UnityEngine;


public class CamFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float smooth = 0.01f;

    [SerializeField] float distanceToTarget;

    Vector3 offset;


    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void Start()
    {
        float angle = transform.rotation.eulerAngles.x * Mathf.Deg2Rad;
        offset = new Vector3(0, -Mathf.Sin(angle) * distanceToTarget, Mathf.Cos(angle) * distanceToTarget);
        Debug.Log(offset);
    }


    void FixedUpdate()
    {
        if (target == null) return;

        var currentPosition = transform.position;
        var targetPosition = target.position - offset;

        var newPosition = Vector3.Lerp(currentPosition, targetPosition, smooth);
        transform.position = newPosition;
    }
}
