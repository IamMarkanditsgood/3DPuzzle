using UnityEngine;

namespace PuzzleMechanic.Systems.PuzzleBuilder
{
        public class ObjectBuilder
        {
                private readonly GameObject[] _objectPieces;
                private readonly GameObject[] _baseHologramPieces;
                private readonly GameObject[] _currentObject;
                private readonly Material _mainMaterial;
        
                public ObjectBuilder(GameObject[] baseHologramPieces,GameObject[] currentObject,GameObject[] objectPieces, Material mainMaterial)
                {
                        _baseHologramPieces = baseHologramPieces;
                        _currentObject = currentObject;
                        _objectPieces = objectPieces;
                        _mainMaterial = mainMaterial;
                }
        
                public void BreakObject(Material newMaterial,Material newBaseMaterial)
                {
                        SwitchMaterial(newMaterial,newBaseMaterial);
                        SwitchPieces(true);
                }

                public void AssembleObject()
                {
                        SwitchMaterial(_mainMaterial);
                        SwitchPieces(false);
                }

                private void SwitchMaterial(Material newMaterial, Material baseMaterial)
                {

                        SwitchMaterialOfAllObjects(newMaterial);
                        foreach (var basePiece in _baseHologramPieces)
                        {
                                basePiece.GetComponent<Renderer>().material = baseMaterial; 
                        }
                }
                private void SwitchMaterial(Material newMaterial)
                {
                        SwitchMaterialOfAllObjects(newMaterial);
                }

                private void SwitchMaterialOfAllObjects(Material newMaterial)
                {
                        foreach (var piece in _currentObject)
                        {
                                piece.GetComponent<Renderer>().material = newMaterial;
                        }
                }
                private void SwitchPieces(bool state)
                {
                        foreach (GameObject piece in _objectPieces)
                        {
                                piece.SetActive(state);
                        }
                }
        }
}