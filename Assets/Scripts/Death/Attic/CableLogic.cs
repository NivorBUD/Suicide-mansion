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
        
        var pos = transform.position;
        pos.z += 0.1f;

        lineRenderer.SetPosition(0, pos);
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

        var portPos = rightPort.gameObject.transform.position;
        portPos.z += 0.1f;

        var pos = transform.position;
        pos.z += 0.1f;

        lineRenderer.SetPosition(1, isActiveCabel ? GetMousePosition() : isReady ? portPos : pos);
    }

    private Vector3 GetMousePosition()
    {
        var mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousepos.z = 0;
        return mousepos;
    }
}
