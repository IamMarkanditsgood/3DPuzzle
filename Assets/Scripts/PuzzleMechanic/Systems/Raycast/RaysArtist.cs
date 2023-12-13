using UnityEngine;

namespace PuzzleMechanic.Systems.Raycast
{
    public class RaysArtist : MonoBehaviour
    {
        [SerializeField] private float _rayLength = 0.35f;
        [SerializeField] private int _angleOffset = 40;
        
        private void Update()
        {
            CreateRay(Vector3.up, Color.white, _rayLength );
            CreateRay(Quaternion.Euler(0, 0, _angleOffset) * Vector3.up, Color.red, _rayLength);
            CreateRay(Quaternion.Euler(0, 0, -_angleOffset) * Vector3.up, Color.green, _rayLength);
            CreateRay(Quaternion.Euler(_angleOffset, 0, 0) * Vector3.up, Color.blue, _rayLength);
            CreateRay(Quaternion.Euler(-_angleOffset, 0, 0) * Vector3.up, Color.yellow, _rayLength);
            
            CreateRay(Quaternion.Euler(_angleOffset, 0, _angleOffset) * Vector3.up, Color.black, _rayLength);
            CreateRay(Quaternion.Euler(-_angleOffset, 0, -_angleOffset) * Vector3.up, Color.magenta, _rayLength);
            CreateRay(Quaternion.Euler(_angleOffset, 0, -_angleOffset) * Vector3.up, Color.cyan, _rayLength);
            CreateRay(Quaternion.Euler(-_angleOffset, 0, _angleOffset) * Vector3.up, Color.gray, _rayLength);
        }
        void CreateRay(Vector3 direction, Color color, float rayLength)
        {
            Ray ray = new Ray(transform.position, direction);
            Debug.DrawRay(ray.origin, ray.direction * rayLength, color);
        }
    }
}