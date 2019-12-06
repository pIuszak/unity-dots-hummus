using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct UnitNavAgentComponent : IComponentData
{
    public float3 finalDestination;
    public NavAgentStatus agentStatus;
    public float3 RandomThreshold;
}

public enum NavAgentStatus
{
    Idle = 0,
    Moving = 1,
}



