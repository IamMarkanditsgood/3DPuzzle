﻿using System.Linq;
using Interfaces;
using PuzzleMechanic.Enums;
using PuzzleMechanic.Interfaces;
using PuzzleMechanic.Systems;
using Systems;
using UnityEngine;

namespace PuzzleMechanic
{
    public class CubeAssembler : MonoBehaviour, IBreakable, IAssemble
    {
        [SerializeField] private Spots _spots = new(); 
        [SerializeField] private GameObject[] _objectPieces;
        [SerializeField] private GameObject[] _basePieces;
        [SerializeField] private Material _newMaterial;
        [SerializeField] private Pieces _pieces = new();

        private ObjectBuilder _objectBuilder;
        private bool _isBroken;

        private void Awake()
        {
            Subscribe();
            Initialize();
        }
        private void OnDestroy()
        {
            UnSubscribe();
        }

        private void Subscribe()
        {
            _pieces.OnSpotPutted += CheckWinScore;
        }

        private void UnSubscribe()
        {
            _pieces.OnSpotPutted -= CheckWinScore;
        }
        
        public void BreakObject()
        {
            if (!_isBroken)
            {
                _pieces.InitializePieces(_objectPieces, _basePieces, _spots);
                _isBroken = true;
                _objectBuilder.BreakObject(_newMaterial);
            }
        }
        public void AssembleObject()
        {
            _objectBuilder.AssembleObject();
            _spots.OffAllSpots();
            ReInitializeObject();
        }

        public void CheckPieceSpotСorrectness(GameObject piece)
        {
            _pieces.CheckPieceSpotСorrectness(piece);
        }

        private void Initialize()
        {
            gameObject.tag = Tags.DestructibleObject.ToString();
            _objectBuilder = new ObjectBuilder(gameObject, _objectPieces, gameObject.GetComponent<Renderer>().material);
        }
        
        private void ReInitializeObject()
        {
            _isBroken = false;
            _pieces.ReInitializePieces();
            Initialize();
        }
        
        private void CheckWinScore()
        {
            int score = _pieces.InRightSlot.Count(slot => slot);

            if (score == _pieces.InRightSlot.Length)
            {
                AssembleObject();
            }
        }
    }
}