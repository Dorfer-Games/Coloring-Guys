using UnityEngine;

public static class GameloopExtensions
{
    public static int CalculateLoopIndex(int level, int toLoop, int max)
    {
        var one = level;  // Например, начинаем с 38
        var two = Mathf.FloorToInt(one / toLoop); // 38 / 5 = 7
        var three = Mathf.Abs(two) + max; // 7 + 3 = 10. Прибавляем для того, что бы первые уровни не считать остаток от числа ниже 3.
        var four = three % max; // 10 % 3 = 1

        return four;
    }
}
