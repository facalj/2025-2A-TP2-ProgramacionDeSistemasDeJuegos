using UnityEngine;

public interface ICharacterFactory
{
    GameObject Create(Vector3 position, Quaternion rotation);
}
