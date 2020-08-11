using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, IItem, IRewindableData<float>
{
    [SerializeField] float shootForce;
    [SerializeField] float cooldown;
    [SerializeField] float energyConsumption;
    
    [SerializeField] GameObject prefabBullet;
    [SerializeField] Transform releasePoint;
    [SerializeField] Transform endPoint;

    bool isActivated = false;
    float timeWithoutShooting = 0f;

    Energy energy;

    BasicDataRewinder<float> rewinder;


    public void Activate(bool value)
    {
        isActivated = value;
    }


    public bool IsActivated()
    {
        return isActivated;
    }


    void Start()
    {
        rewinder = new BasicDataRewinder<float>(GetComponentInParent<Rewindable>(), this);
        energy = GetComponentInParent<Energy>();
    }


    void FixedUpdate()
    {
        timeWithoutShooting += Time.fixedDeltaTime;

        if (isActivated)
        {
            bool sufficientEnergy = energy == null || energy.Consume(energyConsumption);
            if (timeWithoutShooting > cooldown && sufficientEnergy)
            {
                Shoot();
                timeWithoutShooting = 0;
            }
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

        var bulletRigidbody = bulletTransform.GetComponent<Rigidbody>();
        bulletRigidbody.AddForce(shootDirection * shootForce, ForceMode.Impulse);
    }


    public float GetRewindableData()
    {
        return timeWithoutShooting;
    }

    public void SetRewindableData(float data)
    {
        timeWithoutShooting = data;
    }
}
