using System;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct PlayerInputComponent : IComponentData {

    public bool LeftClick;
    public bool RightClick; 
}
