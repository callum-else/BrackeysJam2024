using Assets.PlayerInteraction;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Ships
{
    public class ShipPathModule : MonoBehaviour, IShipPathModule, IRecievePrimaryInteraction
    {
        private LineRenderer lineRenderer;
        private List<Vector3> path = new();
        private int currIndex = 0;

        private UnityEvent pathStartedEvent;
        public UnityEvent PathStartedEvent => pathStartedEvent ??= new();

        public List<Vector3> Path => path;

        private void Awake()
        {
            lineRenderer = GetComponentInChildren<LineRenderer>();
        }

        public void GetMouseWorldPosition(Vector3 position)
        {
            if (path.Count == 0 || Vector3.Distance(position, path.Last()) > 2f)
            {
                path.Add(position);
                lineRenderer.positionCount++;
            }

            lineRenderer.SetPosition(lineRenderer.positionCount - 1, position);
        }

        public void OnInteractionStart()
        {
            path.Clear();
            lineRenderer.positionCount = 0;
            currIndex = 0;
            PathStartedEvent.Invoke();
        }

        public void OnInteractionEnd()
        {
            //Debug.Log("End");
        }

        public Vector3? GetNext()
        {
            if (currIndex + 1 >= path.Count)
                return null;

            return path[++currIndex];
        }
    }
}