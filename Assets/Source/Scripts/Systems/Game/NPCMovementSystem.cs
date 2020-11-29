using Kuhpik;
using Supyrb;
using UnityEngine;

public class NPCMovementSystem : GameSystem, IIniting
{
    [Header("Distances")]
    [SerializeField] float downDistance;
    [SerializeField] float rayStep;

    [Header("Rays")]
    [SerializeField] LayerMask mask;
    [SerializeField] int maxRays = 12; //Кол-во проверок
    [SerializeField] int safeRays = 6; //Если до пропасти есть хотя бы столько проверок, то можно бежать вперёд
    [SerializeField] int raysToJump = 3; //Какое расстояния мы можем перепрыгнуть

    void IIniting.OnInit()
    {
        //Все персонажи кроме индекса 0 (игрока)
        for (int i = 1; i < game.characters.Length; i++)
        {
            var character = game.characters[i];
            game.characters[i].rotationValue = 0;
            RaycastDirection(character, character.rigidbody.transform.forward, out var indexBeforeEmpty, out var emptyCombo, out var checks);

            //Прыгаем только если рядом с пропастью и после пропасти есть куда преземляться
            if (indexBeforeEmpty == 0 && checks > raysToJump)
            {
                //Прыгаем
                //Мы вызываем сигнал для общения с другими системами. Слушают его или нет, нам не важно.
                //Это позволит делать отключаемый прыжок. Единственная проблема, что расчёты бота всё же думают что прыжок всегда есть.
                Signals.Get<JumpReadySignal>().Dispatch(i);
            }

            //Бежим вперёд, если достаточно далеко до границы арены. Например 12 прверок и 3 empty
            else if (checks - emptyCombo > safeRays)
            {
                //Бежим
                continue;
            }

            //Ищем куда бы повернуть
            else
            {
                RaycastDirection(character, character.rigidbody.transform.right, out var rightIndex, out var rightCombo, out var rightChecks);
                RaycastDirection(character, character.rigidbody.transform.right * -1, out var leftIndex, out var leftCombo, out var leftChecks);

                var rotationDirection = rightChecks - rightCombo > leftChecks - leftCombo ? 1 : -1;
                game.characters[i].rotationValue = rotationDirection;
            }
        }
    }

    void RaycastDirection(Character character, Vector3 direction, out int indexBeforeEmpty, out int emptyCombo, out int checks)
    {
        indexBeforeEmpty = -1; //Начиная с какого луча начались пробелы
        emptyCombo = 0; //Как много "пустых" лучей впереди подряд
        checks = 0; //Всего лучей проверено

        //Можно ли бежать вперёд?
        for (int i = 0; i < maxRays; i++)
        {
            var startPoint = character.rigidbody.position + (Vector3.up * 0.5f) + (direction * (rayStep * (i + 1)));

            //Луч попал. Впереди есть место
            if (Physics.Raycast(startPoint, Vector2.down, downDistance, mask))
            {
                Debug.DrawRay(startPoint, Vector3.down, Color.green, 0.1f);
                emptyCombo = 0;
            }

            //Луч не попал
            else
            {
                Debug.DrawRay(startPoint, Vector3.down, Color.red, 0.1f);
                if (indexBeforeEmpty == -1) indexBeforeEmpty = i;
                emptyCombo++;

                //Тут уже не перепрыгнуть, анализируем
                if (emptyCombo > raysToJump) break;
            }

            //Предположим indexBeforeEmpty начался с индекса 0, тогда комбо будет равно raysToJump + 1, а checks будет raysToJump
            checks++;
        }
    }
}
