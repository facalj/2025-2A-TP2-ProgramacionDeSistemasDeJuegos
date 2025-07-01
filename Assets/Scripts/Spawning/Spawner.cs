using UnityEngine;

public class Spawner : MonoBehaviour
{
    [field: SerializeField] public ICharacterFactory CharacterFactory { get; set; }

    public GameObject Spawn(Vector3 position, Quaternion rotation)
    {
        Debug.Log("Spawned!");
        return CharacterFactory.Create(position, rotation);
    }
}
