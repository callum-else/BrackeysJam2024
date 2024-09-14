using Assets.Global;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.PlayerInteraction
{
    public class PlayerInteractionModule : MonoBehaviour, IOnInteractPrimary
    {
        private IGlobalEventProcessorModule gepm;
        private Camera mainCamera;
        private bool canInteract;
        private Plane worldPlane;
        private bool isInputDown = false;
        private IRecievePrimaryInteraction target;

        private void Awake()
        {
            gepm = GetComponent<IGlobalEventProcessorModule>();
            mainCamera = Camera.main;
            worldPlane = new Plane(Vector3.up, Vector3.zero);
        }

        private void OnEnable()
        {
            gepm.OnStageChanged.AddListener(HandleOnStageChanged);
            gepm.GameOverEvent.AddListener(HandleGameOverEvent);
        }

        private void OnDisable()
        {
            gepm.OnStageChanged.RemoveListener(HandleOnStageChanged);
            gepm.GameOverEvent.RemoveListener(HandleGameOverEvent);
        }

        private void HandleOnStageChanged(int stage)
        {
            if (stage != 1) return;
            canInteract = true;
        }

        private void HandleGameOverEvent(IGameOverEventArgs args)
        {
            canInteract = false;
            if (target != null)
            {
                target.OnInteractionEnd();
                target = null;
            }
        }

        public void OnInteractPrimary(InputValue input)
        {
            if (!canInteract) return;

            isInputDown = input.isPressed;

            if (!isInputDown)
            {
                if (target != null)
                {
                    target.OnInteractionEnd();
                    target = null;
                }
                return;
            }

            var ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (!Physics.Raycast(ray, out var hit, 500f))
                return;

            var module = hit.collider.GetComponentInParent<IRecievePrimaryInteraction>();
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