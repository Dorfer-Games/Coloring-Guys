using UnityEngine;

public static class GameloopExtensions
{
    // Например, level = 16, toLoop = 5, max = 3.
    // Где ToLoop это кол-во уровней в цикле, а max это кол-во циклов.
    // Т.е. если у нас 16 уровень, при toLoop 5 и max 3, то нам вернёт уже первый цикл.
    // levelInLoop вернём номер уровня в цикле. Где 0 это первый уровень в цикле.
  
    public static int CalculateLoopIndex(int level, int toLoop, int max, out int levelInLoop)
    {
        var fullLoops = level / toLoop;
        var loop = fullLoops % max;

        levelInLoop = (level % toLoop);		
        return loop;
    }
}
