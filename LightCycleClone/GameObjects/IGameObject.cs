using System;
using Microsoft.Xna.Framework.Graphics;

namespace LightCycleClone.GameObjects
{
    interface IGameObject
    {
        Guid Id { get; }
    }
}