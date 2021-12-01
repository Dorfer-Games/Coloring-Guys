using UnityEngine;

public static class GameloopExtensions
{
    // Например, level = 16, toLoop = 5, max = 3.
    // Где ToLoop это кол-во уровней в цикле, а max это кол-во циклов.
    // Т.е. если у нас 16 уровень, при toLoop 5 и max 3, то нам вернёт уже первый цикл.
  
    public static int CalculateLoopIndex(int level, int toLoop, int max)
    {
        var one = level;
        var two = Mathf.FloorToInt(one / toLoop);
        var three = Mathf.Abs(two) + max;
        var four = three % max;

        return four;
    }
}
