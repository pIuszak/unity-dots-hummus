using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Collections;
using Random = UnityEngine.Random;

public class NavAgentMovementSystem : JobComponentSystem
{
    public static uint Seed = 1;

    private static float GetRnd()
    {
        Seed++;
        return new Unity.Mathematics.Random(Seed).NextFloat() * 1;
    }
    private static float3 ApplyThreshold(float3 float3, float rand)
    {
        return new float3(float3.x + rand,0,float3.x + rand  );
    }

    public struct NavAgentMovementJob : IJobForEach<Translation, UnitNavAgent>
    {
        public float dT;

        public void Execute(ref Translation position, [ReadOnly] ref UnitNavAgent agent)
        {
           // Debug.Log(GetRnd() + " RND ");
           // agent.finalDestination = ApplyThreshold(agent.finalDestination, GetRnd());
            float distance = math.distance(agent.finalDestination, position.Value);
            float3 direction = math.normalize(agent.finalDestination - position.Value);
            float speed = 5;
            if(!(distance < 1) && agent.agentStatus == NavAgentStatus.Moving)
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
        return job.Schedule(this, inputDeps);
    }



}
