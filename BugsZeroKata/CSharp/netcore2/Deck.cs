using System.Collections.Generic;

namespace Trivia
{
    public class Deck
    {
        private readonly Stack<string> _questions;

        public Deck(Category category, IReadOnlyCollection<string> questions)
        {
            _questions = new Stack<string>(questions);
            Category = category;
        }

        public Category Category { get; }

        public string Pick() => _questions.Pop();
    }
}