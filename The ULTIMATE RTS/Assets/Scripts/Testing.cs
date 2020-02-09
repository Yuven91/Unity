using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Rendering;

public class Testing : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Mesh mesh;
    [SerializeField] private Material material;
    void Start()
    {
        EntityManager entityManager = World.Active.EntityManager;

        EntityArchetype characterArchetype = entityManager.CreateArchetype(
            typeof(LevelComponent),
            typeof(Translation),
            typeof(RenderMesh),
            typeof(LocalToWorld),
            typeof(MoveSpeedComponent)

            );

        NativeArray<Entity> entityArray = new NativeArray<Entity>(100000, Allocator.Temp);

        entityManager.CreateEntity(characterArchetype, entityArray);

        for (int i = 0; i < entityArray.Length; i++)
        {
            Entity entity = entityArray[i];

            entityManager.SetComponentData(entity, 
                new LevelComponent { 
                    level = Random.Range(10, 20f) 
                }
            );
            entityManager.SetComponentData(entity, 
                new MoveSpeedComponent { 
                    moveSpeed = Random.Range(0.5f, 3f) 
                }
             );

            entityManager.SetComponentData(entity,
                new Translation
                {
                    Value = new Unity.Mathematics.float3(Random.Range(-8, 8f), Random.Range(-5, 5f), 0)
                }
             );
            entityManager.SetSharedComponentData(entity, 
                new RenderMesh {
                    mesh = mesh,
                    material = material

                });
        }

        entityArray.Dispose();
    }
}
