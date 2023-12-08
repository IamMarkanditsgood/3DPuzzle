using UnityEngine;

namespace Systems
{
    public class ObjectManipulator
    {
        public void MoveObject(GameObject movableObject, Ray ray, float rayDistance)
        {
            if (movableObject != null)
            {
                Vector3 rayPoint = ray.GetPoint(rayDistance);
                movableObject.transform.position = rayPoint;
            }
        }

        public void RotateObject()
        {
            //TODO IF NECESSARY THEN IMPLEMENT OBJECT ROTATE MECHANIC
        }
    }
}