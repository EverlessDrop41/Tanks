using UnityEngine;

public class TankAiMovement : MonoBehaviour
{

    public float TankLerpSpeed = 1f;
    // public float Acceleration = 1f;
    public float MoveSpeed = 10f;
    public Transform Target;
    public float TargetStopRadius = 10f;

    Rigidbody RB;
    NavMeshAgent NMA;

    void Start()
    {
        RB = GetComponent<Rigidbody>();
        NMA = GetComponent<NavMeshAgent>();
    }
    void FixedUpdate()
    {
        if (NMA.SetDestination(Target.position))
        {
            bool targetOutOfRange = IsTargetOutOfRange();

            if (targetOutOfRange)
            {
                Vector3 nextTarget = NMA.steeringTarget;
                Vector3 direction = nextTarget - transform.position;
                direction.y = 0;
                RotateTank(direction);
                RB.velocity = transform.forward * MoveSpeed;
            }
            else
            {
                RB.velocity = Vector3.zero;
            }

        }
        else
        {
            Debug.LogWarning("Failed to set path to target");
        }
    }

    public bool IsTargetOutOfRange()
    {
        Collider[] objectsInRange = Physics.OverlapSphere(transform.position, TargetStopRadius);
        foreach (Collider obj in objectsInRange)
        {
            if (obj.gameObject.tag == Target.gameObject.tag)
            {
                return false;
            }
        }

        return true;
    }

    private void RotateTank(Vector3 moveVector)
    {
        float tankRotationAngle = Mathf.Atan2(moveVector.x, moveVector.z) * Mathf.Rad2Deg;

        var tankRotation = Quaternion.Euler(transform.localEulerAngles.x, tankRotationAngle, transform.localEulerAngles.z);
        transform.rotation = Quaternion.Lerp(transform.rotation, tankRotation, TankLerpSpeed);
    }

    public void OnDrawGizmos()
    {
        try
        {
            var path = NMA.path;
            for (int i = 0; i < path.corners.Length - 1; i++)
            {
                //Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
            }

            Gizmos.DrawSphere(transform.position, TargetStopRadius);
        }
        catch (System.Exception e)
        {
            
        }
        
    }
}
