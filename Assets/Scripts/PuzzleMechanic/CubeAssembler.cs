using System;
using System.Linq;
using Enums;
using Interfaces;
using Systems;
using UnityEngine;

namespace PuzzleMechanic
{
    public class CubeAssembler : MonoBehaviour, IBreakable, IAssemble
    {
        [SerializeField] private GameObject[] _objectPieces;
        [SerializeField] private Vector3[] _piecesSlotsPosition;
        [SerializeField] private Quaternion[] _piecesSlotsRotation;
        [SerializeField] private bool[] _inRightSlot;
        [SerializeField] private Material _newMaterial;
        [SerializeField] private float _allowedError = 0.2f;
    
        private ObjectBuilder _objectBuilder;
        private bool _isBroken;

        private void Awake()
        {
            Initialize();
        }

        public void BreakObject()
        {
            if (!_isBroken)
            {
                _isBroken = true;
                _objectBuilder.BreakObject(_newMaterial);
            }
        }
        public void AssembleObject()
        {
            _objectBuilder.AssembleObject();
            ReInitializeObject();
        }
        public void CheckPieceSpotСorrectness(GameObject piece)
        {
            int arrayIndex = GetArrayIndex(piece);
        
            if (Vector3.Distance(piece.transform.position, _piecesSlotsPosition[arrayIndex]) <= _allowedError)
            {
                PutPieceOnSpot(piece, arrayIndex);
                CheckWinScore();
            }
        }
    
        private void Initialize()
        {
            InitializePieces();
            gameObject.tag = Tags.DestructibleObject.ToString();
            _objectBuilder = new ObjectBuilder(gameObject, _objectPieces, gameObject.GetComponent<Renderer>().material);
        }

        private void InitializePieces()
        {
            int numberOfPieces = _objectPieces.Length;
            _inRightSlot = new bool[numberOfPieces];
            _piecesSlotsPosition = new Vector3[numberOfPieces];
            _piecesSlotsRotation = new Quaternion[numberOfPieces];
        
            for (int i = 0; i < _objectPieces.Length; i++)
            {
                _objectPieces[i].tag = Tags.PiecesOfObject.ToString();
                _piecesSlotsPosition[i] = _objectPieces[i].transform.position;
                _piecesSlotsRotation[i] = _objectPieces[i].transform.rotation;
            }
        }
    
        private void ReInitializeObject()
        {
            _isBroken = false;
            foreach (var objectPiece in _objectPieces)
            {
                objectPiece.GetComponent<Rigidbody>().isKinematic = false;
                objectPiece.GetComponent<Collider>().enabled = true;
            }
            Initialize();
        }

        private void PutPieceOnSpot(GameObject piece, int objectIndex)
        {
            piece.GetComponent<Rigidbody>().isKinematic = true;
            piece.GetComponent<Collider>().enabled = false;
            piece.transform.position = _piecesSlotsPosition[objectIndex];
            piece.transform.rotation = _piecesSlotsRotation[objectIndex];
            _inRightSlot[objectIndex] = true;
        }
        private void CheckWinScore()
        {
            int score = _inRightSlot.Count(slot => slot);

            if (score == _inRightSlot.Length)
            {
                AssembleObject();
            }
        }
    
        private int GetArrayIndex(GameObject piece)
        {
            int index = Array.IndexOf(_objectPieces,piece);
        
            return index;
        }
    }
}