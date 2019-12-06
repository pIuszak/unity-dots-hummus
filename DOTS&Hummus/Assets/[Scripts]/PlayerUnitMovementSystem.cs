using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Collections;

public class PlayerUnitMovementSystem : JobComponentSystem
{
    public static uint Seed = 1;

    public struct PlayerUnitMovementJob : IJobForEach<PlayerInputComponent, UnitNavAgentComponent, PlayerUnitSelect>
    {
        public float dT;
        public float3 mousePos;

        public void Execute
            ([ReadOnly] ref PlayerInputComponent pInputComponent, ref UnitNavAgentComponent navAgentComponent, [ReadOnly] ref PlayerUnitSelect selected)
        {
           if (pInputComponent.RightClick)
            {

                navAgentComponent.finalDestination = mousePos;
                navAgentComponent.agentStatus = NavAgentStatus.Moving;
            }
        }
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var mousePos = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if(hit.collider != null)
            {
                mousePos = new float3(hit.point.x, 0, hit.point.z);
            }
        }

        var job = new PlayerUnitMovementJob
        {
            mousePos = mousePos
        };
        return job.Schedule(this, inputDeps);
    }


}
