using Assets.PlayerInteraction;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Ships
{
    public class ShipPathModule : MonoBehaviour, IRecievePrimaryInteraction
    {
        private LineRenderer lineRenderer;
        private List<Vector3> path = new();

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        public void GetMouseWorldPosition(Vector3 position)
        {
            if (path.Count > 0 && Vector3.Distance(position, path.Last()) < 1.5f)
                return;

            path.Add(position);
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, position);
            Debug.Log("Position Added");
        }

        public void OnInteractionStart()
        {
            path.Clear();
            lineRenderer.positionCount = 0;
        }
    }
}