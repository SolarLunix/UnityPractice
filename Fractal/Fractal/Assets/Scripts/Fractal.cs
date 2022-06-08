using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fractal : MonoBehaviour
{

    public Mesh mesh;
    public Material material;
    public int maxDepth = 4;

    private int depth = 0;
    public float childScale = 0.5f;

    public float maxRotationSpeed = 60;
    private float rotationSpeed;

    private void Start()
    {
        rotationSpeed = Random.Range(-maxRotationSpeed, maxRotationSpeed);
        gameObject.AddComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshRenderer>().material = material;

        if (depth == 0)
        {
            GameObject root = new GameObject("Fractal Root");
            root.AddComponent<MeshFilter>().mesh = mesh;
            root.AddComponent<MeshRenderer>().material = material;
            root.transform.parent = this.transform;
            root.transform.localPosition = Vector3.down;
        }

        if (depth < maxDepth)
        {
            StartCoroutine(CreateChildren());
        }
    }

    private void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    private static Vector3[] childDirections = {
        Vector3.up,
        Vector3.right,
        Vector3.left,
        Vector3.forward,
        Vector3.back
    };

    private static Quaternion[] childOrientations = {
        Quaternion.identity,
        Quaternion.Euler(0f, 0f, -90f),
        Quaternion.Euler(0f, 0f, 90f),
        Quaternion.Euler(90f, 0f, 0f),
        Quaternion.Euler(-90f, 0f, 0f)
    };

    private IEnumerator CreateChildren()
    {
        for (int i = 0; i < childDirections.Length; i++)
        {
            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
            new GameObject("Fractal Child").AddComponent<Fractal>().
                Initialize(this, i);
        }
    }

    private void Initialize(Fractal parent, int childIndex)
    {
        maxRotationSpeed = parent.maxRotationSpeed;
        mesh = parent.mesh;
        material = parent.material;
        maxDepth = parent.maxDepth;
        depth = parent.depth + 1; ;
        childScale = parent.childScale;
        transform.parent = parent.transform;
        transform.localScale = Vector3.one * childScale;
        transform.localPosition = childDirections[childIndex] * (0.5f + 0.5f * childScale);
        transform.localRotation = childOrientations[childIndex];
    }
}
