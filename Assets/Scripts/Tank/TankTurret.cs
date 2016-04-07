using UnityEngine;
using System.Collections;

public class TankTurret : MonoBehaviour
{
    public float TurretLerpSpeed = 0.1f;
    public Transform BarrelEnd;
    public GameObject Bullet;

    void Update()
    {
        if (Input.GetButtonDown("Fire"))
        {
            Instantiate(Bullet, BarrelEnd.position, BarrelEnd.rotation);
        }

        RotateTurret();
    }

    private void RotateTurret()
    {
        Vector3 cursorWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //cursorWorldPos.y = transform.position.y;

        Ray ray = new Ray(cursorWorldPos, Camera.main.transform.forward);

        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        hit.point = new Vector3(hit.point.x, transform.position.y, hit.point.z);

        var lookPos = hit.point - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(lookPos);

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, TurretLerpSpeed);
    }

   /* public void OnDrawGizmos()
    {
        Vector3 cursorWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //cursorWorldPos.y = transform.position.y;

        Ray ray = new Ray(cursorWorldPos, Camera.main.transform.forward);

        RaycastHit hit;
        Physics.Raycast(ray, out hit);

        Gizmos.DrawCube(new Vector3(hit.point.x, transform.position.y, hit.point.z), new Vector3(1,1,1));
    }*/
}
