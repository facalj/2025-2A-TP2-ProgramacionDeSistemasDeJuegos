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
        if (character is ISetup<CharacterModel> setupCharacter)
            setupCharacter.Setup(characterModel);


        if (!result.TryGetComponent<PlayerController>(out var controller))
            controller = result.AddComponent<PlayerController>();
        if (controller is ISetup<IPlayerControllerModel> setupController)
            setupController.Setup(controllerModel);

        var animator = result.GetComponentInChildren<Animator>();
        if (animator != null)
            animator.runtimeAnimatorController = animatorController;
        else
            Debug.Log("Animator controller not found!!!!!");


        return result;
    }
}