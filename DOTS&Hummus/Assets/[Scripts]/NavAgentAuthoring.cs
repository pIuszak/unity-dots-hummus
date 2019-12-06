using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

// ReSharper disable once InconsistentNaming
[RequiresEntityConversion]
[AddComponentMenu("DOTS Samples/SpawnFromEntity/NavAgentAuthoring")]
[ConverterVersion("joe", 1)]
public class NavAgentAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        Debug.Log("xd");
        var data = new UnitNavAgentComponent {  };
        dstManager.AddComponentData(entity, data);
    }

}
