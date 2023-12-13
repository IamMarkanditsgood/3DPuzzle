using System;
using UnityEngine;

namespace PuzzleMechanic.Systems
{
    [Serializable]
    public class Exploder
    {
        [SerializeField] private float _explosionForce;
        [SerializeField] private float _explosionRadius;
        [SerializeField] private Transform _explosionPosition;
        public void Explode(GameObject[] pieces)
        {
            Rigidbody[] piecesRb = new Rigidbody[pieces.Length];
            for (int i = 0; i < pieces.Length; i++)
            {
                piecesRb[i] = pieces[i].GetComponent<Rigidbody>();
            }

            for (var index = 0; index < piecesRb.Length; index++)
            {
                Rigidbody piece = piecesRb[index];
                // Check if the Rigidbody exists
                if (piece != null)
                {
                    // Calculate direction from the explosion position to the piece
                    Vector3 direction = piece.transform.position - _explosionPosition.transform.position;

                    // Calculate distance to determine the strength of the force
                    float distance = direction.magnitude;

                    // Calculate falloff based on explosion radius (optional)
                    float falloff = 1 - Mathf.Clamp01(distance / _explosionRadius);

                    // Apply explosion force to the piece with falloff
                    piece.AddForce(direction.normalized * _explosionForce * falloff, ForceMode.Impulse);
                }
            }
        }
    }
}