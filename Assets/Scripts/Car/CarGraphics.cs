using Data;
using UnityEngine;

namespace Car
{
    public class CarGraphics : MonoBehaviour
    {
        [Header("REFERENCES")]
        [SerializeField] private GameObject graphicsParent;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Transform explosionTransform;
        [SerializeField] private GameObject[] vfxAccelerations;

        /// <summary>
        /// Changes the car graphics material to match the one the player selected
        /// </summary>
        public void UpdateMaterial()
        {
            meshRenderer.sharedMaterial = Registry.gameConfig.playerCarMaterials[DataManager.data.carMaterialIndex];
        }
        
        /// <summary>
        /// Hides the car graphics and display an vfx explosion at its position
        /// </summary>
        public void AnimateExplosion()
        {
            graphicsParent.SetActive(false);
            Instantiate(Registry.gameConfig.vfxExplosion, explosionTransform.position, Quaternion.identity);
        }

        /// <summary>
        /// Toggle the display of the vfx acceleration particules 
        /// </summary>
        /// <param name="doToggle">TRUE display acceleration particules - FALSE hide acceleration particules</param>
        public void ToggleAccelerationParticules(bool doToggle)
        {
            foreach (GameObject vfxAcceleration in vfxAccelerations)
                vfxAcceleration.SetActive(doToggle);
        }
    }
}