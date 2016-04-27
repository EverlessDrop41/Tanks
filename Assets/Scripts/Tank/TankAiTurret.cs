using UnityEngine;
using System.Collections;

public class TankAiTurret : MonoBehaviour {

    public float TurretLerpSpeed = 0.1f;
    public float FireDelay = 1;
    public Transform BarrelEnd;
    public GameObject Bullet;
    public TankAiMovement AiMovement;

    public float currentShotCooldown;

    void Update()
    {
        if (currentShotCooldown <= 0)
        {
            if (!AiMovement.IsTargetOutOfRange())
            {
                Instantiate(Bullet, BarrelEnd.position, BarrelEnd.rotation);
                currentShotCooldown = FireDelay;
            }
        }
        else
        {
            currentShotCooldown -= Time.deltaTime;
        }
        RotateTurret();
    }

    private void RotateTurret()
    {
        Vector3 targetPos = AiMovement.Target.position;
        targetPos.y = transform.position.y;

        Ray ray = new Ray(targetPos, Camera.main.transform.forward);

        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        hit.point = new Vector3(hit.point.x, transform.position.y, hit.point.z);

        var lookPos = targetPos - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(lookPos);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, TurretLerpSpeed);
    }
}
