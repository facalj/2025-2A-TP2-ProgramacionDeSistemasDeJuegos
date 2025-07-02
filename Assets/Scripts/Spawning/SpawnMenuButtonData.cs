using UnityEngine;

[CreateAssetMenu(menuName = "Menu/Spawn Button Config")]
public class SpawnMenuButtonData : ScriptableObject
{
    public string ButtonText;
    [SerializeField] private ScriptableObject factoryAsset;

    public ICharacterFactory Factory => factoryAsset as ICharacterFactory;
}