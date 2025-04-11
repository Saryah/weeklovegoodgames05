using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public GameObject objectToPlace;
    public float gridSize = 1f;
    [SerializeField] List<Color> colors = new List<Color>();
    private GameObject ghostObject;
    private HashSet<Vector3> occupiedPositions = new HashSet<Vector3>();
    private bool canPlace;

    void Update()
    {
        if (objectToPlace == null)
            return;
        UpdateGhostPosition();
        if (Input.GetMouseButtonDown(0) && GameManager.instance.inMenu == false && !PlayerBase.instance.isPurchasingFloor && canPlace)
            PlaceObject();
    }

    public void SetObjectToPlace(GameObject obj)
    {
        objectToPlace = obj;
        CreateGhostObject();
        GameManager.instance.buildMenu.SetActive(false);
        GameManager.instance.inMenu = false;
        GameManager.instance.LockCursor();
        GameManager.instance.player.GetComponent<FirstPersonController>().enabled = true;
    }
    void CreateGhostObject()
    {
        ghostObject = Instantiate(objectToPlace);
        Collider[] colliders =ghostObject.GetComponentsInChildren<Collider>();
        foreach (Collider col in colliders)
            col.enabled = false;
        
        Renderer[] renderers = ghostObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            Material mat = renderer.material;
            Color color = mat.color;
            color.a = 0.5f;
            
            mat.SetFloat("_Mode", 2);
            mat.SetInt("_ScrBlend",(int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = 3000;
            colors.Add(color);
        }
    }

    void UpdateGhostPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 point = hit.point;
            
            Vector3 snappedPos = new Vector3(Mathf.Round(point.x / gridSize) * gridSize,
                Mathf.Round(point.y / gridSize) * gridSize + .5f, 
                Mathf.Round(point.z / gridSize) * gridSize);
            ghostObject.transform.position = snappedPos;

            if (occupiedPositions.Contains(snappedPos) || hit.collider.gameObject.tag != "Floor")
            {
                SetGhostColor(Color.red);
                canPlace = false;
            }
            else
            {
                SetGhostColor(new Color(1f,1f,1f,0.5f));
                canPlace = true;
            }
        }
        
    }

    void SetGhostColor(Color color)
    {
        Renderer[] renderers = ghostObject.GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            Material mat = renderer.material;
            mat.color = color;
        }
    }

    void PlaceObject()
    {
        Vector3 placementPosition = ghostObject.transform.position;

        if (!occupiedPositions.Contains(placementPosition))
        {
            Renderer[] renderers = ghostObject.GetComponentsInChildren<Renderer>();
            for (int i = 0; i < renderers.Length; i++)
            {
                Material mat = renderers[i].material;
                mat.color = colors[i];
            }
            Instantiate(objectToPlace, placementPosition, Quaternion.identity);
            occupiedPositions.Add(placementPosition);
        }
        ghostObject = null;
        objectToPlace = null;
        
    }
}
