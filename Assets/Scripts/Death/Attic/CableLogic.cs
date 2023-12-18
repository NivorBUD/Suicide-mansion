using UnityEditor;
using UnityEngine;

public class CableLogic : MonoBehaviour
{
    public bool isActiveCabel;
    public bool isReady;
    public PortLogic rightPort;
    
    [SerializeField] private ElectricMinigameLogic gameLogic;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, transform.position);
    }

    private void OnMouseDown()
    {
        if (isReady)
            return;

        if (gameLogic.activeCable == null)
        {
            isActiveCabel = true;
            gameLogic.activeCable = this;
        }
    }

    void Update()
    {
        if (gameLogic.activeCable != this)
            isActiveCabel = false;

        lineRenderer.SetPosition(1, isActiveCabel ? GetMousePosition() : isReady ? rightPort.gameObject.transform.position : transform.position);
    }

    private Vector3 GetMousePosition()
    {
        var mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousepos.z = 0;
        return mousepos;
    }
}
