using Kuhpik;
using UnityEngine;

public class JumpSystem : GameSystem, IIniting, IUpdating
{
    [SerializeField] int framesBeforeJumpEnds;
    [SerializeField] bool canRotateWhileJump;

    int framesFromTouch = 200;

    void IIniting.OnInit()
    {
        Physics.gravity = Vector3.down * config.GravityStrength;

        foreach (var character in game.characters)
        {
            character.onCollisionComponent.OnEnter += Grouding;
        }
    }

    void IUpdating.OnUpdate()
    {
        UserInputHandling();
        CharactersRotationHandling();
    }

    void CharactersRotationHandling()
    {
        if (!canRotateWhileJump)
        {
            foreach (var character in game.characters)
            {
                if (character.isJumping) character.rotationValue = 0;
            }
        }
    }

    void UserInputHandling()
    {
        if (Input.GetMouseButtonDown(0))
        {
            framesFromTouch = 0;
        }

        else if (Input.GetMouseButton(0))
        {
            framesFromTouch++;
        }

        else if (Input.GetMouseButtonUp(0))
        {
            //Не прошло достаточно кадров, что бы не считали действие за попытку прыжка
            if (framesFromTouch <= framesBeforeJumpEnds && !game.characters[0].isJumping)
            {
                Jump(0);
            }
        }
    }

    void Jump(int index)
    {
        game.characters[index].rigidbody.AddRelativeForce(Vector3.up * config.JumpStrength, ForceMode.VelocityChange);
        game.characters[index].animator.SetBool("Jumping", true);
        game.characters[index].isJumping = true;
    }

    void Grouding(Transform other, Transform @object)
    {
        if (other.CompareTag("Cell"))
        {
            var character = game.characterDictionary[@object];

            if (character.isJumping)
            {
                character.animator.SetBool("Jumping", false);
                character.isJumping = false;
            }
        }
    }
}
