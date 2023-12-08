using UnityEngine;

namespace PuzzleMechanic.Interfaces
{
    public interface IAssemble
    {
        public void AssembleObject();
        public void CheckPieceSpotСorrectness(GameObject piece);
    }
}