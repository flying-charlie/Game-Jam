using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class TileController : MonoBehaviour // , Tiling.iTile
{
    public string configId;
    TileConfig m_tileConfig;
    GameObject m_ship;
    ShipController m_shipController;
    public Vector2Int? gridPos = null; //bottom left
    public int mass = 1;
    public float health;
    public float maxHealth = 0;
    bool dragging = false;
    public Vector2Int size = new Vector2Int(1, 1);
    public string tileType;


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
        if (!m_shipController.GridPosIsFull(gridPos) && m_shipController.GridPosHasNeibours(gridPos))
        {
            AttachAt(gridPos);
        }
        else
        {
            Debug.Log(gridPos);
            if (m_shipController.GridPosIsFull(gridPos))
            {
                Debug.Log("Position full");
            }
            if (!m_shipController.GridPosHasNeibours(gridPos))
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
        DoJoining();
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
        m_tileConfig = GameObject.FindGameObjectWithTag("config").GetComponent<Config>().tileCfg[configId];
        maxHealth = m_tileConfig.maxHealth;
        health = maxHealth;
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
        mass = size.x * size.y;

        maxHealth = mass * m_tileConfig.maxHealth;
    }

    void DoJoining()
    {
        Dictionary<string, GameObject> neighbours = m_shipController.GetNeighbours((Vector2Int)gridPos);
        GameObject maxMassTile = null;
        string maxMassTileDirection = "";
        int maxMass = 0;
        foreach (KeyValuePair<string, GameObject> pair in neighbours)
        {
            if (pair.Value != null && pair.Value.GetComponent<TileController>().mass > maxMass && m_shipController.CanJoin(pair.Value, Utils.InvertDirection(pair.Key)))
            {
                maxMass = pair.Value.GetComponent<TileController>().mass;
                maxMassTile = pair.Value;
                maxMassTileDirection = pair.Key;
            }
        }
        if (maxMassTile != null)
        {
            maxMassTile.GetComponent<TileController>().Join(Utils.InvertDirection(maxMassTileDirection));
        }
    }

    public void Join(string direction)
    {
        Vector2Int tilePos = (Vector2Int)gridPos;
        if (direction == "down")
        {
            for (int pos = tilePos.x; pos < tilePos.x + size.x; pos++)
            {
                GameObject foundTile = m_shipController.GetTileAt(new Vector2Int(pos, tilePos.y - 1));
                health += foundTile.GetComponent<TileController>().health;
                Destroy(foundTile);
            }
            gridPos += Vector2Int.down;
            size.y += 1;
            OnSizeChange();
        }
        else if (direction == "up")
        {
            for (int pos = tilePos.x; pos < tilePos.x + size.x; pos++)
            {
                GameObject foundTile = m_shipController.GetTileAt(new Vector2Int(pos, tilePos.y + size.y));
                health += foundTile.GetComponent<TileController>().health;
                Destroy(foundTile);
            }
            size.y += 1;
            OnSizeChange();
        }
        else if (direction == "left")
        {
            for (int pos = tilePos.y; pos < tilePos.y + size.y; pos++)
            {
                GameObject foundTile = m_shipController.GetTileAt(new Vector2Int(tilePos.x - 1, pos));
                health += foundTile.GetComponent<TileController>().health;
                Destroy(foundTile);
            }
            gridPos += Vector2Int.left;
            size.x += 1;
            OnSizeChange();
        }
        else if (direction == "right")
        {
            for (int pos = tilePos.y; pos < tilePos.y + size.y; pos++)
            {
                GameObject foundTile = m_shipController.GetTileAt(new Vector2Int(tilePos.x + size.x, pos));
                health += foundTile.GetComponent<TileController>().health;
                Destroy(foundTile);
            }
            size.x += 1;
            OnSizeChange();
        }
        else
        {
            throw new System.Exception("Join only accepts directions \"up\", \"down\", \"left\" and \"right\"");
        }
        DoJoining();
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
                if (health <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    public struct TileConfig
    {
        public float maxHealth;
        public float health;
    }
