
using UnityEngine;
using UnityEngine.UIElements;

public class UpdatePosition : MonoBehaviour
{
    [Header("Properties")] 
    public PolygonCollider2D HoleCollider;
    public PolygonCollider2D GroundCollider;
    public MeshCollider GeneratedMeshCollider;
    private Mesh GeneratedMesh;
    [SerializeField] private float TargetScale = 0.5f;


    [Header("Collider info")] 
    public Collider baseGroundCollider;




    private void Start()
    {
        InitializingCollectableCollisions();
    }

    private void FixedUpdate()
    {
//        Debug.Log("Running");
        if (transform.hasChanged)
        {
            transform.hasChanged = false;
            HoleCollider.transform.position = new Vector2(transform.position.x, transform.position.z);
            HoleCollider.transform.localScale = transform.localScale * TargetScale;
            CreateHole();
            CreateMeshCollider();
        }
    }

    void InitializingCollectableCollisions()
    {
        GameObject[] collectableObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];

        foreach (var collectable in collectableObjects)
        {
            if (collectable.layer.Equals(LayerMask.NameToLayer("Collectable")))
            {
                Physics.IgnoreCollision(collectable.GetComponent<Collider>(), GeneratedMeshCollider, true);
            }
        }
    }


    private void CreateHole()
    {
        Vector2[] Positions = HoleCollider.GetPath(0);
        for (int i = 0; i < Positions.Length; i++)
        {
//            Positions[i] += (Vector2)HoleCollider.transform.position;
            Positions[i] = HoleCollider.transform.TransformPoint(Positions[i]);
        }

        GroundCollider.pathCount = 2;
        GroundCollider.SetPath(1, Positions);

    }

    private void CreateMeshCollider()
    {
        if(GeneratedMesh != null)
            Destroy(GeneratedMesh);
            
        GeneratedMesh = GroundCollider.CreateMesh(true, true);
        GeneratedMeshCollider.sharedMesh = GeneratedMesh;
    }


    private void OnTriggerEnter(Collider other)
    {
        Physics.IgnoreCollision(other, baseGroundCollider, true);
        Physics.IgnoreCollision(other, GeneratedMeshCollider, false);
    }
    
    private void OnTriggerExit(Collider other)
    {
        Physics.IgnoreCollision(other, baseGroundCollider, false);
        Physics.IgnoreCollision(other, GeneratedMeshCollider, true);
    }





}
