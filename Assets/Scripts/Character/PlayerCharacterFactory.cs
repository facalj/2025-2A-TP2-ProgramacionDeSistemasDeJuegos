using UnityEngine;

[CreateAssetMenu(menuName = "Character Factory/Player")]
public class PlayerCharacterFactory : ScriptableObject, ICharacterFactory
{
    [SerializeField] private Character prefab;
    [SerializeField] private CharacterModel characterModel;
    [SerializeField] private PlayerControllerModel controllerModel;
    [SerializeField] private RuntimeAnimatorController animatorController;

    public GameObject Create(Vector3 position, Quaternion rotation)
    {
        var result = Instantiate(prefab.gameObject, position, rotation);

        if (!result.TryGetComponent<Character>(out var character))
            character = result.AddComponent<Character>();
        character.Setup(characterModel);

        if (!result.TryGetComponent<PlayerController>(out var controller))
            controller = result.AddComponent<PlayerController>();
        controller.Setup(controllerModel);

        if (result.TryGetComponent<Animator>(out var animator))
            animator.runtimeAnimatorController = animatorController;

        return result;
    }
}
