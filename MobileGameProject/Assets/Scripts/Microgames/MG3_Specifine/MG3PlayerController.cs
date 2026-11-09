using MicrogameCourse.Microgames;
using UnityEngine;
using UnityEngine.InputSystem;

public class MG3PlayerController : MonoBehaviour
{
    [SerializeField] public MG3GameManager gameManager;
    [SerializeField] public GameObject targetPrefab;

    [Header("Player Input")]
    [SerializeField] public InputActionAsset InputActions;
    private InputAction spawnTarget;

    private void Awake()
    {
        spawnTarget = InputSystem.actions.FindAction("Spawn");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTarget.WasPressedThisFrame())
        {
            SpawnTarget();
        }
    }

    public void SpawnTarget()
    {
        Instantiate(targetPrefab, new Vector3(Random.Range(-2f, 2f), Random.Range(-4f, 3f), 0), Quaternion.identity);
    }
}
