using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int cherries = 0;
    [SerializeField] private int TotalCherries = 5;
    [SerializeField] private Text cherriesText;

    void Start()
    {
        UpdateCherries();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            AudioManager.Instance.PlaySound(AudioType.itemCollect);
            Destroy(collision.gameObject);
            cherries++;
            UpdateCherries();
        }
    }

    private void UpdateCherries()
    {
        cherriesText.text = $"{cherries}/{TotalCherries}";
    }
}
