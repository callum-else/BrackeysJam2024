using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.PlayerInteraction
{
    public class PlayerInteractionModule : MonoBehaviour, IOnInteractPrimary
    {
        private Camera mainCamera;
        private Plane worldPlane;
        private bool isInputDown = false;
        private IRecievePrimaryInteraction target;

        private void Awake()
        {
            mainCamera = Camera.main;
            worldPlane = new Plane(Vector3.up, Vector3.zero);
        }

        public void OnInteractPrimary(InputValue input)
        {
            isInputDown = input.isPressed;

            if (!isInputDown)
            {
                target = null;
                return;
            }

            var ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (!Physics.Raycast(ray, out var hit, 50f))
                return;

            var module = hit.collider.GetComponent<IRecievePrimaryInteraction>();
            if (module == null)
                return;

            if (target != null && target.GetInstanceID() == module.GetInstanceID())
                return;

            target = module;
            target.OnInteractionStart();
        }

        private void FixedUpdate()
        {
            if (!isInputDown) return;
            if (target == null) return;

            var ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (worldPlane.Raycast(ray, out var dist))
                target.GetMouseWorldPosition(ray.GetPoint(dist));
        }
    }
}