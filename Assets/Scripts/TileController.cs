using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileController : MonoBehaviour // , Tiling.iTile
{
    GameObject m_ship;
    ShipController m_shipController;
    public Vector2Int? gridPos = null; //bottom left
    public int Mass;
    bool dragging = false;
    public Vector2Int size = new Vector2Int(1, 2);


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
        Vector2 relativePos = m_shipController.worldPosToGridPos(transform.position, size);
        Vector2Int gridPos = new(Mathf.RoundToInt(relativePos.x), Mathf.RoundToInt(relativePos.y));
        Debug.Log("Attempting attach");
        if (!m_shipController.gridPosIsFull(gridPos) && m_shipController.gridPosHasNeibours(gridPos))
        {
            AttachAt(gridPos);
        }
        else
        {
            Debug.Log(gridPos);
            if (m_shipController.gridPosIsFull(gridPos))
            {
                Debug.Log("Position full");
            }
            if (!m_shipController.gridPosHasNeibours(gridPos))
            {
                Debug.Log("No neibours");
            }
        }
    }

    void AttachAt(Vector2Int gridPos)
    {
        transform.parent = m_ship.transform;
        this.gridPos = gridPos;
        transform.localPosition = new Vector3(gridPos.x, gridPos.y);
        Utils.RotateTowardsLocal(transform, m_ship.transform.rotation.z, 1);
        m_shipController.OnMassChange();
    }

    void Detach()
    {
        gridPos = null;
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
            gridPos = new Vector2Int(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        }
        m_ship = GameObject.FindGameObjectWithTag("ship");
        m_shipController = m_ship.GetComponent<ShipController>();
        OnSizeChange();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnSizeChange()
    {
        transform.localScale = new Vector3(size.x, size.y);
        GetComponent<BoxCollider2D>().size = new Vector2(size.x, size.y);
        if (gridPos != null)
        {
            transform.localPosition = new Vector3((float)(size.x - 1) / 2 + ((Vector2)gridPos).x, (float)(size.y - 1) / 2 + ((Vector2)gridPos).y);
        }
        Mass = size.x * size.y;
        m_shipController.OnMassChange();
    }

    public virtual void attachedUpdate(TileUpdateData data) {}  //use this instead of update

    // public Vector2Int gridPosition{get{
    //     return m_gridPos;
    // }}

    // public IEnumerable<Vector2Int> usedConnections {get{

    // }}

    // public IEnumerable<Vector2Int> openConnections => throw new System.NotImplementedException();
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            Destroy(gameObject);
        }
    }
}
