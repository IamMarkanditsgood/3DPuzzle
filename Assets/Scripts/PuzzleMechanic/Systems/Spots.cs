using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PuzzleMechanic.Systems
{
    [Serializable]
    public class Spots
    {
        [SerializeField] private GameObject _spotPrefab;
        [SerializeField] private Transform _parent;
        [SerializeField] private List<GameObject> _spotsPool = new();
        
        public void PutSpot(int pieceIndex, Vector3[] piecesSlotsPosition)
        {
            GameObject spot = GetFreeSpot();
            spot.transform.position = piecesSlotsPosition[pieceIndex];
            spot.transform.SetParent(_parent);
        }

        public void OffAllSpots()
        {
            foreach (var t in _spotsPool)
            {
                t.SetActive(false);
            }
        }
        
        private GameObject GetFreeSpot()
        {
            foreach (var spot in _spotsPool)
            {
                if (!spot.activeInHierarchy)
                {
                    spot.SetActive(true);
                    return spot;
                }
            }
            GameObject newSpot = CreateNewSpot();
            newSpot.SetActive(true);
            return newSpot;
        }

        private GameObject CreateNewSpot()
        {
            GameObject newSpot = Object.Instantiate(_spotPrefab);
            newSpot.SetActive(false);
            _spotsPool.Add(newSpot);
            return newSpot;
        }
    }
}