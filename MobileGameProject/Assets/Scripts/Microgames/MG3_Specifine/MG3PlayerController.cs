using MicrogameCourse.Microgames;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// ln 48-67 and ln 88-109
/// Trying to get target tap functionality working, but testing does not return expected results.
/// </summary>

public class MG3PlayerController : MonoBehaviour
{
    [SerializeField] public MG3GameManager gameManager;
    [SerializeField] public GameObject targetPrefab;
    private Camera cam;
    RaycastHit hit;
    public MG3Target target;

    [Header("Player Input")]
    [SerializeField] public InputActionAsset InputActions;
    private InputAction debugButton;
    private InputAction clickTap;

    private void Awake()
    {
        debugButton = InputSystem.actions.FindAction("Debug");
        clickTap = InputSystem.actions.FindAction("Clicktap");
        cam = Camera.main;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (debugButton.WasPressedThisFrame())
        {
            // SpawnTarget();
            Debug.Log(PointerWorldPosition());
        }

        Vector2 worldOrigin = new Vector3(0, 0, 0);
        Vector2 targetPoint = PointerWorldPosition();

        transform.position = targetPoint;

        if (clickTap.WasPressedThisFrame())
        {
            // Debug.Log("Tap!");

            // if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), 10))
            // {
            //     Debug.Log("Hit!");
            //     MG3Target hitTarget = hit.collider.GetComponent<MG3Target>();

            //     if (hitTarget.targetType == MG3Target.TargetType.Red && gameManager.playerState == MG3GameManager.TypeState.Red)
            //     {
            //         Destroy(hit.collider);
            //     }
            //     else if (hitTarget.targetType == MG3Target.TargetType.Green && gameManager.playerState == MG3GameManager.TypeState.Green)
            //     {
            //         Destroy(hit.collider);
            //     }
            //     else
            //     {
            //         return;
            //     }
            // }
        }
    }

    public void SpawnTarget()
    {
        Instantiate(targetPrefab, new Vector3(Random.Range(-2f, 2f), Random.Range(-4f, 3f), 0), Quaternion.identity);
    }

    private Vector2 PointerWorldPosition()
    {
        Vector2 screenPosition = Pointer.current.position.ReadValue();
        return cam.ScreenToWorldPoint(screenPosition);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {        
        // if (collision.name == "Target")
        // {
        //     Debug.Log("Target found!");
            
        //     if (clickTap.WasPressedThisFrame())
        //     {
        //         target = collision.GetComponent<MG3Target>();

        //         if (target.targetType == MG3Target.TargetType.Red && gameManager.playerState == MG3GameManager.TypeState.Red)
        //         {
        //             Destroy(collision);
        //         }
        //         else if (target.targetType == MG3Target.TargetType.Green && gameManager.playerState == MG3GameManager.TypeState.Green)
        //         {
        //             Destroy(collision);
        //         }
        //         else
        //         {
        //             return;
        //         }
        //     }
        // }
    }
}
