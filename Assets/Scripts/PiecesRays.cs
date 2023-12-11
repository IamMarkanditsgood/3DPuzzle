using System;
using System.Linq;
using PuzzleMechanic.Enums;
using UnityEngine;

[Serializable]
public class PiecesRays
{
    [SerializeField] private float _rayLenghtAfter = 0.2f;
    [SerializeField] private int _angleOffset = 40;
    [SerializeField] private float _rayLength = 0.35f;
    
    private const int SizeOfArray = 9;


    public bool CreateRays(GameObject currentPieces)
    {
        GetRayLenght(currentPieces);
       bool[] rays = new bool[SizeOfArray];
       rays[0] = CreateRay(Vector3.down, currentPieces, _rayLength );
       rays[1] =CreateRay(Quaternion.Euler(0, 0, _angleOffset) * Vector3.down, currentPieces, _rayLength);
       rays[2] =CreateRay(Quaternion.Euler(0, 0, -_angleOffset) * Vector3.down, currentPieces, _rayLength);
       rays[3] =CreateRay(Quaternion.Euler(_angleOffset, 0, 0) * Vector3.down, currentPieces, _rayLength);
       rays[4] =CreateRay(Quaternion.Euler(-_angleOffset, 0, 0) * Vector3.down, currentPieces, _rayLength);
       
       rays[5] =CreateRay(Quaternion.Euler(_angleOffset, 0, _angleOffset) * Vector3.down, currentPieces, _rayLength);
       rays[6] =CreateRay(Quaternion.Euler(-_angleOffset, 0, -_angleOffset) * Vector3.down, currentPieces, _rayLength);
       rays[7] =CreateRay(Quaternion.Euler(_angleOffset, 0, -_angleOffset) * Vector3.down, currentPieces, _rayLength);
       rays[8] =CreateRay(Quaternion.Euler(-_angleOffset, 0, _angleOffset) * Vector3.down, currentPieces, _rayLength);
       if (rays.Any(t => t == true))
       {
           return true;
       }

       return false;
   }

    private bool CreateRay(Vector3 direction, GameObject piece, float rayLength)
    {
        Collider pieceCollider = piece.GetComponent<Collider>();
        Ray ray = new Ray(piece.transform.position, direction);
        pieceCollider.enabled = false;
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayLength))
        {
            if (hit.collider.gameObject.CompareTag(Tags.UntouchedPiece.ToString()))
            {
                pieceCollider.enabled = true;
                return true;
            }
        }
        pieceCollider.enabled = true;
        return false;
    }

    private void GetRayLenght(GameObject piece)
    {
        _rayLength = piece.transform.localScale.y / 2 + _rayLenghtAfter;
    }
}