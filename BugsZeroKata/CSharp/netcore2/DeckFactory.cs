using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class DeckFactory
    {
        public static IReadOnlyCollection<Deck> Create(
            int nbQuestions,
            IReadOnlyCollection<Category> categories)
        {
            return
                categories
                    .Select(c => CreateDeck(nbQuestions, c))
                    .ToList();
        }

        private static Deck CreateDeck(int nbQuestions, Category category) =>
            new Deck(
                category,
                CreateQuestions(nbQuestions, category));

        private static List<string> CreateQuestions(int nbQuestions, Category c) =>
            Enumerable.Range(0, nbQuestions)
                .Select(i => $"{c.Name} Question {i}")
                .ToList();
    }
}