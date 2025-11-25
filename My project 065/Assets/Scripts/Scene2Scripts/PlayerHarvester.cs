using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHarvester : MonoBehaviour
{
    public float rayDistance = 5f;
    public LayerMask hitMask = ~0;
    public int toolDamage = 1;
    public float hitCoolDown = 0.15f;

    float _nextHitTime;
    Camera _cam;
    Inventory inventory;
    InventoryUI invenUI;
    private void Awake()
    {
        _cam = Camera.main;
        if (inventory == null ) inventory = gameObject.AddComponent<Inventory>();
        invenUI = FindObjectOfType<InventoryUI>();
    }

    private void Update()
    {
        if (invenUI.selectedIndex < 0)
        {
            if (Input.GetMouseButton(0) && Time.time >= _nextHitTime)
            {
                _nextHitTime = Time.time + hitCoolDown;

                Ray ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                if (Physics.Raycast(ray, out var hit, rayDistance, hitMask))
                {
                    var block = hit.collider.GetComponent<Block>();
                    if (block != null)
                    {
                        block.Hit(toolDamage, inventory);

                    }
                }
            }
        }
       
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                Ray ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                if (Physics.Raycast(ray, out var hit, rayDistance, hitMask, QueryTriggerInteraction.Ignore))
                {
                    Vector3Int placePos = AdjacentCellOnHitFace(hit);

                    BlockType selected = invenUI.GetInventorySlot();
                    if (inventory.Consume(selected, 1))
                    {
                        FindObjectOfType<NoiseVoxelMap>().PlaceTile(placePos, selected);
                    }
                }
            }
        }

       
        static Vector3Int AdjacentCellOnHitFace(in RaycastHit hit)
        {
            Vector3 baseCenter = hit.collider.transform.position; 
            Vector3 adjCenter = baseCenter + hit.normal; 
            return Vector3Int.RoundToInt(adjCenter);
        }
    }
}
