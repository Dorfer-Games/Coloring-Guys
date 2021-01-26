using Kuhpik;
using Supyrb;
using UnityEngine;


public class NPCMovementSystem : GameSystem, IFixedUpdating, IIniting
{
    [SerializeField] LevelsOfMistakesScriptableObject levelsOfMistakes;
    [SerializeField] float timeOnMistake = 0.5f;
    [Header("RandomRotation")]
    [SerializeField] float timeOnCheckingSideForRandomRotation;
    [SerializeField] float timeOnRandomRotation;
    [Header("Distances")]
    [SerializeField] float lengthOfCkeckingRay;
    [SerializeField] float stepOfRay;
    [SerializeField] float startOffsetOfRay;

    [Header("Rays")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] int maxRays = 12; //Кол-во проверок
    [SerializeField] int safeRays = 6; //Если до пропасти есть хотя бы столько проверок, то можно бежать вперёд
    [SerializeField] int safeRaysSide = 3; //Безопасное расстояние от края по бокам персонааж.
    [SerializeField] int raysToJump = 3; //Какое расстояния мы можем перепрыгнуть
    [SerializeField] int safeRaySideBeforeJump = 2; //Зато не магия... Для проверки того, есть ли обрывы перед прыжком.

    private float[] passedTimeOnCheckRightSide;
    private float[] passedTimeOnCheckLeftSide;
    private float[] passedTimeOnDoingMistake;
    private float[] mistakeDelay;

    void IIniting.OnInit()
    {
        passedTimeOnCheckLeftSide = new float[game.characters.Length];
        passedTimeOnCheckRightSide = new float[game.characters.Length];
        passedTimeOnDoingMistake = new float[game.characters.Length];
        mistakeDelay = new float[game.characters.Length];
        for (int i = 1; i < passedTimeOnCheckLeftSide.Length; i++)
        {
            passedTimeOnCheckRightSide[i] = -3f;
            passedTimeOnCheckRightSide[i] = -3f;
            mistakeDelay[i] = levelsOfMistakes.GetMistakeDelay(game.characters[i].levelOfMistake);
            passedTimeOnDoingMistake[i] = Random.Range(0f, mistakeDelay[i]);
        }
    }
    void IFixedUpdating.OnFixedUpdate()
    {
        //Все персонажи кроме индекса 0 (игрока)
        for (int i = 1; i < game.characters.Length; i++)
        {
            var character = game.characters[i];
            if (character.isDeath)
            {
                passedTimeOnCheckRightSide[i] = -1;
                passedTimeOnCheckLeftSide[i] = -1;
                passedTimeOnDoingMistake[i] = -1f;
                continue;
            }

            game.characters[i].rotationValue = 0;

            RaycastDirection(character, character.rigidbody.transform.forward, out var indexBeforeEmpty, out var emptyComboForward, out var checks, out var empty);
            RaycastDirection(character, (character.rigidbody.transform.forward + character.rigidbody.transform.right)/2, out var indexBeforeEmpty_right, out var emptyCombo_right, out var checks_right, out var empty_right);
            RaycastDirection(character, (character.rigidbody.transform.forward + character.rigidbody.transform.right * -1)/2, out var indexBeforeEmpty_left, out var emptyCombo_left, out var checks_left, out var empty_left);
            RaycastDirection(character, character.rigidbody.transform.right, out var rightIndex, out var rightCombo, out var rightChecks, out var rightEmpty);
            RaycastDirection(character, character.rigidbody.transform.right * -1, out var leftIndex, out var leftCombo, out var leftChecks, out var leftEmpty);

            if (game.characters[i].levelOfMistake != 0)
            {
                passedTimeOnDoingMistake[i] += Time.deltaTime;
            }


            
            if (passedTimeOnDoingMistake[i] >= mistakeDelay[i])
            {
               if (passedTimeOnDoingMistake[i] >= mistakeDelay[i] + timeOnMistake)
               {
                   passedTimeOnDoingMistake[i] -= mistakeDelay[i] + timeOnMistake;
               }
                // Do mistekes
                if (AvoidingObstacles(i, emptyComboForward, emptyCombo_right, emptyCombo_left, empty_left, empty_right))
                {
                    passedTimeOnCheckLeftSide[i] = 0;
                    passedTimeOnCheckRightSide[i] = 0;
                    continue;
                }
                //if (RandomPathing(i, leftEmpty, rightEmpty)) { continue; }
            }
             else
             {
                if (AvoidingObstacles(i, emptyComboForward, emptyCombo_right, emptyCombo_left, empty_right, empty_left))
                {
                    passedTimeOnCheckLeftSide[i] = 0;
                    passedTimeOnCheckRightSide[i] = 0;
                    continue;
                }
                //if(RandomPathing(i, rightEmpty, leftEmpty)) { continue; }
             
            }
        }
    }

    private bool RandomPathing(int i, int rightEmpty, int leftEmpty)
    {
        if (rightEmpty == 0)
        {
            passedTimeOnCheckRightSide[i] += Time.deltaTime; 
            if (passedTimeOnCheckRightSide[i] < timeOnCheckingSideForRandomRotation)
            {
                return false;
            }
            else
            {
                if(timeOnRandomRotation + timeOnCheckingSideForRandomRotation < passedTimeOnCheckRightSide[i])
                {
                    passedTimeOnCheckRightSide[i] = 0;
                }
                var rotationDirection = 1;
                game.characters[i].rotationValue = rotationDirection;
                return true;
            }
            
        }
        else if (leftEmpty == 0)
        {
            passedTimeOnCheckLeftSide[i] += Time.deltaTime;
            if (passedTimeOnCheckLeftSide[i] < timeOnCheckingSideForRandomRotation)
            {
                return false;
            }
            else
            {
                if (timeOnRandomRotation + timeOnCheckingSideForRandomRotation < passedTimeOnCheckLeftSide[i])
                {
                    passedTimeOnCheckLeftSide[i] = 0;
                }
                var rotationDirection = -1;
                game.characters[i].rotationValue = rotationDirection;
                return true;
            }
        }
        return false;
    }

    private bool AvoidingObstacles(int i, int emptyComboForward, int emptyCombo_right, int emptyCombo_left, int empty_right, int empty_left)
    {
        if (emptyComboForward > safeRays || emptyCombo_right > safeRays || emptyCombo_left > safeRays)
        {

            var rotationDirection = empty_right < empty_left ? 1 : -1;
            game.characters[i].rotationValue = rotationDirection;
            /*if (backindex == 0 && indexBeforeEmpty != 0 && !sidesIsSafe)
            {
                rotationDirection = backEmpity < empty ? 1 : -1;
                game.characters[i].rotationValue = rotationDirection;
            }*/
            return true;
        }
        return false;
    }

    void RaycastDirection(Character character, Vector3 direction, out int indexBeforeEmpty, out int emptyCombo, out int checks, out int empty)
    {
        indexBeforeEmpty = 10000; //Начиная с какого луча начались пробелы
        emptyCombo = 0; //Как много "пустых" лучей впереди подряд
        checks = 0; //Всего лучей проверено
        empty = 0;

        //Можно ли бежать вперёд?
        for (int i = 0; i < maxRays; i++)
        {
            var startPoint = character.rigidbody.position + (direction * startOffsetOfRay) + (Vector3.up * 0.5f) + (direction * (stepOfRay * (i + 1)));
            var index = i;
            try
            {
                //Луч попал. Впереди есть место и клетка не опускается
                if (Physics.Raycast(startPoint, Vector2.down, out var hit, lengthOfCkeckingRay, groundLayer) && !game.cellDictionary[hit.transform.parent].IsGoingToGoUp)
                {
#if UNITY_EDITOR
                    Debug.DrawLine(startPoint, startPoint + Vector3.down, Color.green, 0.1f, false);
#endif
                    emptyCombo = 0;
                }
                //Луч не попал
                else
                {
#if UNITY_EDITOR
                    Debug.DrawLine(startPoint, startPoint + Vector3.down, Color.red, 0.1f, false);
#endif
                    if (indexBeforeEmpty == 10000) indexBeforeEmpty = index;
                    emptyCombo++;
                    empty++;

                    //Тут уже не перепрыгнуть, анализируем
                    if (emptyCombo > raysToJump) break;
                }

                //Предположим indexBeforeEmpty начался с индекса 0, тогда комбо будет равно raysToJump + 1, а checks будет raysToJump
                checks++;
            }
            catch
            {

            }
        }
    }
}
