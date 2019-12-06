using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct AABB : IComponentData
{
    public float3 min;
    public float3 max;
}
