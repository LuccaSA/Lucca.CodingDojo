using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Deck
    {
        public Category Category { get; }
        private LinkedList<string> _questions;

        public Deck(Category category, LinkedList<string> questions)
        {
            _questions = questions;
            Category = category;
        }

        public string Pick()
        {
            var question = _questions.First();
            _questions.RemoveFirst();
            return question;
        }
    }
}