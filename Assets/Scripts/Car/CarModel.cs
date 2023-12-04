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

        public void UpdateMaterial(int materialIndex)
        {
            meshRenderer.sharedMaterial = Registry.gameConfig.playerCarMaterials[materialIndex];
        }
    }
}
