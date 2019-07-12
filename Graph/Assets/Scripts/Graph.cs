using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Rendering;


public class Graph : MonoBehaviour
{
    const float pi = Mathf.PI;
    
    public GraphFunctionName function;

    static public GraphFunction[] functions = {
        SineFunction,
        Sine2DFunction,
        MultiSineFunction,
        MultiSine2DFunction,
        Ripple,
        Cylinder,
        Sphere,
        Torus
    };

    [SerializeField] private Mesh mesh;
    [SerializeField] private Material material;

    [Range(10, 100)] public int resolution = 100;

    private EntityManager entityManager;

    void Awake()
    {
        entityManager = World.Active.EntityManager;
        EntityArchetype entityArchetype = entityManager.CreateArchetype
        (
            typeof(Translation),
            typeof(RenderMesh),
            typeof(LocalToWorld),
            typeof(Scale),
            typeof(DefaultPosition)
        );

        float step = 2f / resolution;
        Scale scale = new Scale();
        scale.Value = step;

        Translation position = new Translation();
        position.Value.y = 0f;

        DefaultPosition place = new DefaultPosition();

        NativeArray<Entity> points = new NativeArray<Entity>(resolution * resolution, Allocator.Temp);
        entityManager.CreateEntity(entityArchetype, points);

        RenderMesh renderMesh = new RenderMesh { mesh = mesh, material = material };

        for (int i = 0, z = 0; i < points.Length; z++)
        {
            position.Value.z = (z + 0.5f) * step - 1f;
            place.v = position.Value.z;
            for (int x = 0; x < resolution; x++, i++)
            {
                position.Value.x = (x + 0.5f) * step - 1f;
                place.u = position.Value.x;
                Entity entity = points[i];
                entityManager.SetSharedComponentData(entity, renderMesh);
                entityManager.SetComponentData(entity, scale);
                entityManager.SetComponentData(entity, position);
                entityManager.SetComponentData(entity, place);
            }
        }
    }

    static Translation SineFunction(float u, float v, float t, Translation pos)
    {
        pos.Value.x = u;
        pos.Value.z = v;

        float y = Mathf.Sin(pi * (u + t));
        pos.Value.y = y;

        return pos;
    }

    static Translation Sine2DFunction(float u, float v, float t, Translation pos)
    {
        pos.Value.x = u;
        pos.Value.z = v;

        float y = Mathf.Sin(pi * (u + t));
        y += Mathf.Sin(pi * (v + t));
        y *= 0.5f;

        pos.Value.y = y;

        return pos;
    }

    static Translation MultiSineFunction(float u, float v, float t, Translation pos)
    {
        pos.Value.x = u;
        pos.Value.z = v;


        float y = Mathf.Sin(pi * (u + t));
        pos.Value.y = y;

        return pos;
    }

    static Translation MultiSine2DFunction(float u, float v, float t, Translation pos)
    {
        pos.Value.x = u;
        pos.Value.z = v;


        float y = 4f * Mathf.Sin(pi * (u + v + t * 0.5f));
        y += Mathf.Sin(pi * (u + t));
        y += Mathf.Sin(2f * pi * (v + 2f * t)) * 0.5f;
        y *= 1f / 5.5f;
        pos.Value.y = y;

        return pos;
    }

    static Translation Ripple(float u, float v, float t, Translation pos)
    {
        pos.Value.x = u;
        pos.Value.z = v;

        float d = Mathf.Sqrt(u * u + v * v);
        float y = Mathf.Sin(pi * (4f * d - t));
        y /= 1f + 10f * d;
        pos.Value.y = y;

        return pos;
    }

    static Translation Cylinder(float u, float v, float t, Translation pos)
    {
        float r = 0.8f + Mathf.Sin(pi * (6f * u + 2f * v + t)) * 0.2f;

        pos.Value.x = r * Mathf.Sin(pi * u);
        pos.Value.y = v;
        pos.Value.z = r * Mathf.Cos(pi * u);

        return pos;
    }

    static Translation Sphere(float u, float v, float t, Translation pos)
    {
        float r = 0.8f + Mathf.Sin(pi * (6f * u + t)) * 0.1f;
        r += Mathf.Sin(pi * (4f * v + t)) * 0.1f;
        float s = r * Mathf.Cos(pi * 0.5f * v);

        pos.Value.x = s * Mathf.Sin(pi * u);
        pos.Value.y = r * Mathf.Sin(pi * 0.5f * v);
        pos.Value.z = s * Mathf.Cos(pi * u);

        return pos;
    }

    static Translation Torus(float u, float v, float t, Translation pos)
    {
        float r1 = 0.65f + Mathf.Sin(pi * (6f * u + t)) * 0.1f;
        float r2 = 0.2f + Mathf.Sin(pi * (4f * v + t)) * 0.05f;
        float s = r2 * Mathf.Cos(pi * v) + r1;
        pos.Value.x = s * Mathf.Sin(pi * u);
        pos.Value.y = r2 * Mathf.Sin(pi * v); 
        pos.Value.z = s * Mathf.Cos(pi * u);
        return pos;
    }
}
