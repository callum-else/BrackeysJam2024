﻿using UnityEngine;

namespace Assets.Global
{
    public interface IShipCrashedEventArgs
    {
        Vector3 Location { get; set; }
        int Value { get; set; }
    }
}