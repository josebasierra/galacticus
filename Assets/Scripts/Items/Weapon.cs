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
    float timeWithoutShooting = 0f;

    //time rewind...
    LimitedStack<float> timeWithoutShootingRegister;
    Rewindable rewindable;


    public void Activate(bool value)
    {
        isActivated = value;
    }


    public bool IsActivated()
    {
        return isActivated;
    }


    void OnEnable()
    {
        if (rewindable == null)
        {
            rewindable = GetComponentInParent<Rewindable>();
            timeWithoutShootingRegister = new LimitedStack<float>(rewindable.MaxCapacity());
        }
        rewindable.OnRewind += OnRewind;
        rewindable.OnRecord += OnRecord;
    }


    void OnDisable()
    {
        rewindable.OnRewind -= OnRewind;
        rewindable.OnRecord -= OnRecord;
    }


    void FixedUpdate()
    {
        timeWithoutShooting += Time.fixedDeltaTime;
        if (isActivated && timeWithoutShooting > cooldown)
        {
            Shoot();
            timeWithoutShooting = 0;
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


    void OnRewind(float time)
    {
        timeWithoutShooting = timeWithoutShootingRegister.Top();
        timeWithoutShootingRegister.Pop();
    }


    void OnRecord()
    {
        timeWithoutShootingRegister.Push(timeWithoutShooting);
    }
}
