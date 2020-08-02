using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IItem
{
    [SerializeField] float shootForce;
    [SerializeField] float cooldown;
    
    [SerializeField] GameObject prefabBullet;
    [SerializeField] Transform releasePoint;
    [SerializeField] Transform endPoint;

    bool isActivated = false;
    bool isOnCooldown = false;


    public void Activate(bool value)
    {
        isActivated = value;
    }


    void FixedUpdate()
    {
        if (isActivated && !isOnCooldown)
        {
            Shoot();
            isOnCooldown = true;
            Invoke(nameof(EnableWeapon), cooldown);
        }
    }


    void Shoot()
    {
        Transform bulletTransform = Instantiate(prefabBullet).transform;
        //bulletTransform.tag = transform.parent.gameObject.tag;

        bulletTransform.position = releasePoint.position;
        bulletTransform.LookAt(endPoint);
        bulletTransform.Rotate(new Vector3(90, 0, 0));

        Vector3 shootDirection = (endPoint.position - releasePoint.position).normalized;
        bulletTransform.GetComponent<Rigidbody>().AddForce(shootDirection * shootForce, ForceMode.Impulse);
    }

    void EnableWeapon()
    {
        isOnCooldown = false;
    }
}
