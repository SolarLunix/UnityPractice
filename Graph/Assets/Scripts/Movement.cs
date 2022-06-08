using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine.UI;

public class Movement : ComponentSystem
{
    protected override void OnUpdate()
    {
        Graph graph = GameObject.Find("Graph").GetComponent<Graph>();
        int func = GameObject.Find("Dropdown").GetComponent<Dropdown>().value;

        Entities.ForEach((ref Translation translation, ref DefaultPosition position) => {
            float u = position.u;
            float v = position.v;
            float t = Time.time;

            translation = Graph.functions[func](u, v, t, translation);
        });
    }
}
