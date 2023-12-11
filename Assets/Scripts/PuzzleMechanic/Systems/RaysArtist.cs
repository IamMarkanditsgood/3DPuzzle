using UnityEngine;

namespace PuzzleMechanic.Systems
{
    public class RaysArtist : MonoBehaviour
    {
        [SerializeField] private float _rayLength = 0.35f;
        [SerializeField] private int _angleOffset = 40;
        
        private void Update()
        {
            CreateRay(Vector3.down, Color.white, _rayLength );
            CreateRay(Quaternion.Euler(0, 0, _angleOffset) * Vector3.down, Color.red, _rayLength);
            CreateRay(Quaternion.Euler(0, 0, -_angleOffset) * Vector3.down, Color.green, _rayLength);
            CreateRay(Quaternion.Euler(_angleOffset, 0, 0) * Vector3.down, Color.blue, _rayLength);
            CreateRay(Quaternion.Euler(-_angleOffset, 0, 0) * Vector3.down, Color.yellow, _rayLength);
            
            CreateRay(Quaternion.Euler(_angleOffset, 0, _angleOffset) * Vector3.down, Color.black, _rayLength);
            CreateRay(Quaternion.Euler(-_angleOffset, 0, -_angleOffset) * Vector3.down, Color.magenta, _rayLength);
            CreateRay(Quaternion.Euler(_angleOffset, 0, -_angleOffset) * Vector3.down, Color.cyan, _rayLength);
            CreateRay(Quaternion.Euler(-_angleOffset, 0, _angleOffset) * Vector3.down, Color.gray, _rayLength);
        }
        void CreateRay(Vector3 direction, Color color, float rayLength)
        {
            Ray ray = new Ray(transform.position, direction);
            Debug.DrawRay(ray.origin, ray.direction * rayLength, color);
        }
    }
}