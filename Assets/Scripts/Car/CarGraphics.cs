using UnityEngine;

namespace Car
{
    public class CarGraphics : MonoBehaviour
    {
        [Header("REFERENCES")]
        [SerializeField] private GameObject graphicsParent;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Transform explosionTransform;

        public void UpdateMaterial(int materialIndex)
        {
            meshRenderer.sharedMaterial = Registry.gameConfig.playerCarMaterials[materialIndex];
        }
        
        public void AnimateExplosion()
        {
            graphicsParent.SetActive(false);
            Instantiate(Registry.gameConfig.vfxExplosion, explosionTransform.position, Quaternion.identity);
        }
    }
}