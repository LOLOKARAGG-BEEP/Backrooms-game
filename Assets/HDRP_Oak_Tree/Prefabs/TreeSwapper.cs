using UnityEngine;

[RequireComponent(typeof(Terrain))]
public class TreeSwapper : MonoBehaviour
{
    public GameObject[] TreePrototypes;

    void Awake()
    {
        Terrain terrain = GetComponent<Terrain>();
        TerrainData data = terrain.terrainData;

        foreach (var instance in data.treeInstances)
        {
            GameObject tree = Instantiate(
                TreePrototypes[instance.prototypeIndex],
                Vector3.Scale(instance.position, data.size) + terrain.transform.position,
                Quaternion.Euler(0, instance.rotation * Mathf.Rad2Deg, 0),
                transform);

            tree.transform.localScale = new Vector3(
                instance.widthScale,
                instance.heightScale,
                instance.widthScale);
        }

        treeInstances = data.treeInstances;  // сохраняем массив «рисованных» деревьев
        data.treeInstances = new TreeInstance[0];  // удаляем все «рисованные» деревья
    }
    private TreeInstance[] treeInstances;
    private void OnDestroy()
    {
        Terrain terrain = GetComponent<Terrain>();
        TerrainData data = terrain.terrainData;
        data.treeInstances = treeInstances;  // восстанавливаем массив «рисованных» деревьев
    }

}

