using UnityEngine;

namespace Car
{
    public class CarModel : MonoBehaviour
    {
        [Header("REFERENCES")] 
        public MeshRenderer meshRenderer;
    
        private void FixedUpdate()
        {
            transform.Rotate(0, 0.5f, 0);
        }

        /// <summary>
        /// Changes the car graphics material to match given material index
        /// </summary>
        public void UpdateMaterial(int materialIndex)
        {
            meshRenderer.sharedMaterial = Registry.gameConfig.playerCarMaterials[materialIndex];
        }
    }
}
