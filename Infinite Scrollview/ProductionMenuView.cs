using UnityEngine;
using UnityEngine.UI;

public class ProductionMenuView : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollView;

    // Event to notify the controller when new items need to be added
    public event System.Action OnScrollNearBottom;

    void Update()
    {
        // Check if the scroll view is near the bottom
        if (scrollView.verticalNormalizedPosition <= 0.05f)
        {
            // If scrolled near bottom, notify the controller to add new items
            OnScrollNearBottom?.Invoke();
        }
    }

    // Method to update the UI with new items
    public void AddItemToView(GameObject itemPrefab, Sprite itemImage, string itemName)
    {
        var product = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity, scrollView.content);
        product.GetComponentInChildren<Image>().sprite = itemImage;
        product.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = itemName;
    }
}
