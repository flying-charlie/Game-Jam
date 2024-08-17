using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileController : MonoBehaviour // , Tiling.iTile
{
    GameObject m_ship;
    ShipController m_shipController;
    public Vector2Int? m_gridPos = null;
    bool dragging = false;
    public float ROTATION_SNAP_SPEED;
    public float POSITION_SNAP_SPEED;


    protected virtual void OnMouseDrag()
    {
        if (!dragging)
        {
            dragging = true;
            OnStartDrag();
        }
        transform.position = Utils.GetMousePosition();
    }

    protected virtual void OnStartDrag()
    {
        Detach();
    }

    protected virtual void OnEndDrag()
    {
        Debug.Log("Drag ended");
        Vector2 relativePos = m_shipController.worldPosToGridPos(transform.position);
        Vector2Int gridPos = new(Mathf.RoundToInt(relativePos.x), Mathf.RoundToInt(relativePos.y));
        Debug.Log("Attempting attach");
        if (!m_shipController.gridPosIsFull(gridPos) && m_shipController.gridPosHasNeibours(gridPos))
        {
            AttachAt(gridPos);
            Debug.Log("Attached");
        }
        else
        {
            Debug.Log("Attach failed");
        }
    }

    void AttachAt(Vector2Int gridPos)
    {
        transform.parent = m_ship.transform;
        m_gridPos = gridPos;
        transform.localPosition = new Vector3(gridPos.x, gridPos.y);
        Utils.RotateTowardsLocal(transform, m_ship.transform.rotation.z, 1);
        m_shipController.OnMassChange();
    }

    void Detach()
    {
        m_gridPos = null;
        transform.parent = null;
        m_shipController.OnMassChange();
    }

    void OnMouseUp()
    {
        if (dragging)
        {
            dragging = false;
            OnEndDrag();
        }
    }

    // Start is called before the first frame update
    protected void Start()
    {
        if (transform.parent == null)
        {
            transform.Rotate(Vector3.forward, Random.Range((float)0, (float)360));
        }
        else
        {
            m_gridPos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        }
        m_ship = GameObject.FindGameObjectWithTag("ship");
        m_shipController = m_ship.GetComponent<ShipController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void attachedUpdate(TileUpdateData data) {}  //use this instead of update

    // public Vector2Int gridPosition{get{
    //     return m_gridPos;
    // }}

    // public IEnumerable<Vector2Int> usedConnections {get{

    // }}

    // public IEnumerable<Vector2Int> openConnections => throw new System.NotImplementedException();
    void OnCollisionEnter2D(Collision2D collision2D)
    {
        Destroy(gameObject);
    }
}
