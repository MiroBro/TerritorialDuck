using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildHandler : MonoBehaviour
{
    public Tilemap previewMap;
    public Tile greenTile;
    public Tile redTile;
    public Transform buildSpot;

    public Transform toBuild;
    public SpriteRenderer[] sg;
    public Transform buildParent;

    public static bool ableToBuild;
    public CapsuleCollider2D buildColl;

    public void InstBuilding(Enums.Buildings building)
    {
        References.Instance.soundHandler.PlayBuySound();
        GameObject build = References.Instance.buildAssets.GetBuild(building);
        var inst = Instantiate(build, buildSpot, buildParent);
        toBuild = inst.transform;
        sg = inst.GetComponent<BuildInfo>().spriteRenderers;
        buildColl = inst.GetComponent<BuildInfo>().coll;
        buildColl.enabled = false;

        foreach (var sgs in sg)
        {
            sgs.color = new Color(1, 1, 1, 0.5f);
        }
        ableToBuild = true;
    }

    private void Update()
    {
        if (ableToBuild)
        {
            ShowPreview();

            if ( GetAcceptBtnInput() && toBuild != null && canPlace)
            {
                toBuild.parent = buildParent;
                buildColl.enabled = true;
                toBuild = null;

                foreach (var sgs in sg)
                {
                    sgs.color = new Color(1, 1, 1, 1);
                }
                previewMap.ClearAllTiles();
                References.Instance.soundHandler.PlayBuildSound();
                ableToBuild = false;
            }
            else if ((Input.GetMouseButtonDown(2) || Input.GetKeyDown(KeyCode.Escape)) && toBuild != null)
            {
                Destroy(toBuild.gameObject);
                toBuild = null;
                previewMap.ClearAllTiles();
                References.Instance.soundHandler.PlayHoverBtn();
                ableToBuild = false;
            } else if (GetAcceptBtnInput() && !canPlace)
            {
                References.Instance.soundHandler.PlayErrorSound();
            }
        }
    }

    private bool GetAcceptBtnInput()
    {
        return (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E));
    }

    public static bool canPlace;
    private void ShowPreview()
    {
        Vector3Int coord = previewMap.WorldToCell(buildSpot.position);

        Vector3 cornerOfCord = previewMap.CellToWorld(coord);
        Vector3 centerOfCord = new Vector3(cornerOfCord.x + previewMap.cellSize.x / 2, cornerOfCord.y + previewMap.cellSize.y / 2, 0);

        if (toBuild != null)
        {
            toBuild.position = centerOfCord;
        }

        previewMap.ClearAllTiles();
        if (IsOverlappingColl())
        {
            canPlace = false;
            previewMap.SetTile(coord, redTile);
        } else
        {
            canPlace = true;
            previewMap.SetTile(coord, greenTile);
        }
    }

    private bool IsOverlappingColl()
    {
        var colls = Physics2D.OverlapCapsuleAll(((Vector2)buildColl.transform.position) + buildColl.offset, buildColl.size * buildColl.transform.localScale, buildColl.direction, 0);

        foreach (var c in colls)
        {
            if (c.CompareTag("Building"))
                return true;
        }
        return false;
    }
}
