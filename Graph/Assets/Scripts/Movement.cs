using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;

public class Movement : ComponentSystem
{
    protected override void OnUpdate()
    {
        Graph graph = GameObject.Find("Graph").GetComponent<Graph>();
        Entities.ForEach((ref Translation translation, ref DefaultPosition position) => {
            float u = position.u;
            float v = position.v;
            float t = Time.time;

            translation = Graph.functions[(int)graph.function](u, v, t, translation);
        });
    }
}
