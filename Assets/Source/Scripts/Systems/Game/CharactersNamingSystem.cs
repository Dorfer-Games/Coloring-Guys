using Kuhpik;

public class CharactersNamingSystem : GameSystem, IIniting
{
    public string[] names;
    void IIniting.OnInit()
    {
        for (int i = 1; i < game.characters.Length; i++)
        {
            //game.characters[i].rigidbody.name = $"AI #{i}";
            game.characters[i].rigidbody.name = names[i];
        }

        game.characters[0].rigidbody.name = "Player";
    }
}
