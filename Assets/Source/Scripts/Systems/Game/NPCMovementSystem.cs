using Kuhpik;
using Supyrb;
using UnityEngine;

public class NPCMovementSystem : GameSystem, IFixedUpdating
{
    [Header("Distances")]
    [SerializeField] float downDistance;
    [SerializeField] float rayStep;
    [SerializeField] float rayOffset;

    [Header("Rays")]
    [SerializeField] LayerMask mask;
    [SerializeField] int maxRays = 12; //Кол-во проверок
    [SerializeField] int safeRays = 6; //Если до пропасти есть хотя бы столько проверок, то можно бежать вперёд
    [SerializeField] int safeRaysSide = 3; //Безопасное расстояние от края по бокам персонааж.
    [SerializeField] int raysToJump = 3; //Какое расстояния мы можем перепрыгнуть
    [SerializeField] int safeRaySideBeforeJump = 2; //Зато не магия... Для проверки того, есть ли обрывы перед прыжком.


    
    void IFixedUpdating.OnFixedUpdate()
    {
        //Все персонажи кроме индекса 0 (игрока)
        for (int i = 1; i < game.characters.Length; i++)
        {
            var character = game.characters[i];
            if (character.isJumping || character.isDeath) continue;

            game.characters[i].rotationValue = 0;

            RaycastDirection(character, character.rigidbody.transform.forward, out var indexBeforeEmpty, out var emptyCombo, out var checks, out var empty);
            RaycastDirection(character, character.rigidbody.transform.right, out var rightIndex, out var rightCombo, out var rightChecks, out var rightEmpty);
            RaycastDirection(character, character.rigidbody.transform.right * -1, out var leftIndex, out var leftCombo, out var leftChecks, out var leftEmpty);
            RaycastDirection(character, character.rigidbody.transform.forward * -1, out var backindex, out var backEmpityCombo, out var leftChbackChecks, out var backEmpity);

            var sidesIsSafe = (rightIndex < 0 || rightIndex > safeRaySideBeforeJump) && (leftIndex < 0 || leftIndex > safeRaySideBeforeJump);

            //Прыгаем только если рядом с пропастью и после пропасти есть куда преземляться и сбоку нет пропасти
            if (indexBeforeEmpty == 0 && checks > raysToJump && sidesIsSafe)
            {
                //Прыгаем
                //Мы вызываем сигнал для общения с другими системами. Слушают его или нет, нам не важно.
                //Это позволит делать отключаемый прыжок. Единственная проблема, что расчёты бота всё же думают что прыжок всегда есть.
                Signals.Get<JumpReadySignal>().Dispatch(i);
            }

            else
            {
                    //Бежим вперёд, если достаточно далеко до границы арены. Например 12 прверок и 3 empty
                    //Из-за неровной генерации хексов у нас часто на границах бывают кейсы с 101010 проверками...
                    //Поэтому проверяем ещё по сторонам сразу
                    if (checks - emptyCombo > safeRays && sidesIsSafe)
                    {
                        //Бежим
                        continue;
                    }

                    //Ищем куда бы повернуть
                    else
                    {
                        var rotationDirection = rightEmpty < leftEmpty ? 1 : -1;
                        game.characters[i].rotationValue = rotationDirection;
                        if (backindex == 0 && indexBeforeEmpty != 0 && !sidesIsSafe)
                        {
                            rotationDirection = backEmpity < empty ? 1 : -1;
                            game.characters[i].rotationValue = rotationDirection;
                        }
                    }
            }
        }
    }

    void RaycastDirection(Character character, Vector3 direction, out int indexBeforeEmpty, out int emptyCombo, out int checks, out int empty)
    {
        indexBeforeEmpty = -1; //Начиная с какого луча начались пробелы
        emptyCombo = 0; //Как много "пустых" лучей впереди подряд
        checks = 0; //Всего лучей проверено
        empty = 0;

        //Можно ли бежать вперёд?
        for (int i = 0; i < maxRays; i++)
        {
            var startPoint = character.rigidbody.position + (direction * rayOffset) + (Vector3.up * 0.5f) + (direction * (rayStep * (i + 1)));
            var index = i;
            try
            {
                //Луч попал. Впереди есть место и клетка не опускается
                if (Physics.Raycast(startPoint, Vector2.down, out var hit, downDistance, mask) && !game.cellDictionary[hit.transform.parent].IsUp)
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
                    if (indexBeforeEmpty == -1) indexBeforeEmpty = index;
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
