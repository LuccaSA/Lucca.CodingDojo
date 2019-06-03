namespace Trivia
{
    public class Category
    {
        public string Name { get; }

        public Category(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}