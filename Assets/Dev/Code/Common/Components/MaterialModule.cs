using UnityEngine;

namespace Assets.Common
{
    public class MaterialModule : MonoBehaviour, IMaterialModule
    {
        public void ApplyUniqueMaterial(Renderer renderer)
        {
            var mat = new Material(renderer.material);
            renderer.material = mat;
        }
    }
}
