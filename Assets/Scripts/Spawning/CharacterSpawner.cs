using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [SerializeField] private Character prefab;
    [SerializeField] private CharacterModel characterModel;
    [SerializeField] private PlayerControllerModel controllerModel;
    [SerializeField] private RuntimeAnimatorController animatorController;

    public void Spawn()
    {
        var result = Instantiate(prefab, transform.position, transform.rotation);
        if (!result.TryGetComponent(out Character character))
            character = result.gameObject.AddComponent<Character>();
        character.Setup(characterModel);

        if (!result.TryGetComponent(out PlayerController controller))
            controller = result.gameObject.AddComponent<PlayerController>();
        controller.Setup(controllerModel);

        var animator = result.GetComponentInChildren<Animator>();
        if (!animator)
            animator = result.gameObject.AddComponent<Animator>();
        animator.runtimeAnimatorController = animatorController;
    }
}