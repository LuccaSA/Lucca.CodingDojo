namespace Trivia
{
    public class Player
    {
        public string Name { get; }

        public Player(string name)
        {
            Name = name;
        }

        public static implicit operator Player(string name)
        {
            return new Player(name);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}