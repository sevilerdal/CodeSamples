using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
public class ProductionMenuController : MonoBehaviour
{
    public static ProductionMenuController Instance { get; private set; }

    [SerializeField] private ProductionMenuModel productionMenuModel;
    [SerializeField] private ProductionMenuView productionMenuView;
    [SerializeField] private GameObject itemPrefab;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        productionMenuView.OnScrollNearBottom += AddItems;
    }

    private void OnDisable()
    {
        productionMenuView.OnScrollNearBottom -= AddItems;
    }

    // Adds items from the pool to the UI
    public void AddItems()
    {
        productionMenuModel.PoolItems();

        foreach (var item in productionMenuModel.ItemPool)
        {
            var itemModel = item.GetComponent<ProductionItemModel>();
            productionMenuView.AddItemToView(itemPrefab, itemModel.ItemImage, itemModel.ItemName);
        }
    }
}
