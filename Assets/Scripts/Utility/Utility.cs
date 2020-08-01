using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public static class Physics2DUtility
    {
        public static bool IsOnGround(Vector2 position, Collider2D collider)
        {
            var contactPoints = new List<ContactPoint2D>();
            collider.GetContacts(contactPoints);

            foreach (var contact in contactPoints)
            {
                if (contact.point.y < position.y - 0.1f) return true;
            }

            return false;
        }
    }

    public static class PhysicsUtility
    {
        public static bool IsOnGround(Collider collider)
        {
            var startPosition = collider.bounds.center;
            bool value = Physics.Raycast(startPosition, Vector3.down, out RaycastHit hit, collider.bounds.extents.y + 0.1f);
            
            Debug.DrawRay(startPosition, Vector3.down * (collider.bounds.extents.y + 0.1f));

            return value;
        }


        public static Vector3 ComputeRequiredForce(Vector3 currentVelocity, Vector3 desiredVelocity, float timeToAchieveSpeed, float mass)
        {
            if (desiredVelocity.magnitude <= 0) return Vector3.zero;

            var avgForce = (mass * desiredVelocity.magnitude) / timeToAchieveSpeed;
            var requiredForce = (desiredVelocity - currentVelocity)/desiredVelocity.magnitude * 2 * avgForce;

            return requiredForce;
        }
    }
}



