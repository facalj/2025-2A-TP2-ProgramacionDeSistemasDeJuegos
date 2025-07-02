using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpawnMenuBuilder : MonoBehaviour
{
    [SerializeField] private SpawnMenuGroup config;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform layoutParent;
    [SerializeField] private Spawner spawner;

    private void Start()
    {
        foreach (var buttonData in config.Buttons)
        {
            var buttonGO = Instantiate(buttonPrefab, layoutParent);
            var button = buttonGO.GetComponent<Button>();
            var text = buttonGO.GetComponentInChildren<TMP_Text>();
            text.text = buttonData.ButtonText;

            var factory = buttonData.Factory;
            button.onClick.AddListener(() =>
            {
                spawner.CharacterFactory = factory;
                spawner.Spawn(Vector3.zero, Quaternion.identity);
            });
        }
    }
}