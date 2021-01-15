using Kuhpik;
using Supyrb;
using UnityEngine;

public class JumpSystem : GameSystem, IIniting, IUpdating
{
    [SerializeField] int framesBeforeJumpEnds;
    [SerializeField] int maxTouchDelta;
    [SerializeField] bool canRotateWhileJump;

    int framesFromTouch = 200;
    Vector2 firstTouchPos;

    void IIniting.OnInit()
    {
        Signals.Get<JumpReadySignal>().AddListener(Jump);
        Physics.gravity = Vector3.down * config.GetValue(EGameValue.GravitySTR);

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
        /*if (Input.GetMouseButtonDown(0))
        {
            framesFromTouch = 0;
            firstTouchPos = Input.mousePosition;
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
                if (Vector2.Distance(firstTouchPos, Input.mousePosition) < maxTouchDelta)
                {
                    Jump(0);
                }
            }
        }*/
    }

    void Jump(int index)
    {
        game.characters[index].rigidbody.AddRelativeForce(Vector3.up * config.GetValue(EGameValue.JumpSTR), ForceMode.VelocityChange);
        game.characters[index].animator.SetBool("Jumping", true);
        game.characters[index].isJumping = true;
        game.characters[index].audioComponent.DisabledAudio();
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
                character.audioComponent.EnabledAudio();
                character.jumpPlayerComponent.Jump = false;
            }
        }
    }
}
