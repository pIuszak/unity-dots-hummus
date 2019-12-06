using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;
using Random = UnityEngine.Random;

public class NavAgentMovementSystem : JobComponentSystem
{
    private bool isVirgin = true;

    public struct NavAgentMovementJob : IJobForEach<Translation, UnitNavAgentComponent>
    {
        public float dT;

        public void Execute(ref Translation position, [ReadOnly] ref UnitNavAgentComponent agentComponent)
        {
            float3 fd = new float3(agentComponent.finalDestination.x + agentComponent.RandomThreshold.x,
                agentComponent.finalDestination.y + agentComponent.RandomThreshold.y,
                agentComponent.finalDestination.z + agentComponent.RandomThreshold.z
            );
            float distance = math.distance(fd, position.Value);
            float3 direction = math.normalize(fd - position.Value);
            float speed = 5;
            if (!(distance < 1) && agentComponent.agentStatus == NavAgentStatus.Moving)
            {
                position.Value += direction * speed * dT;
            }
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new NavAgentMovementJob
        {
            dT = Time.DeltaTime,
        };
        isVirgin = false;
        return job.Schedule(this, inputDeps);
    }

    protected override void OnCreate()
    {
        isVirgin = true;
    }


}