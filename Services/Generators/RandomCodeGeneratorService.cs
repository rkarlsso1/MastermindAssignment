using MastermindAssignment.Domain.Interfaces;

namespace MastermindAssignment.Services.Generators
{
    //service to generate a random secret code
    public class RandomCodeGeneratorService : ICodeGeneratorService
    {
        private readonly Random _random;

        //initialize the random number generator
        public RandomCodeGeneratorService()
        {
            _random = new Random();
        }

        //generate a secret code as a list of integers
        public List<int> GenerateCode(int length, int minValue, int maxValue)
        {
            var code = new List<int>(length);

            for (int i = 0; i < length; i++)
            {
                code.Add(_random.Next(minValue, maxValue + 1));
            }

            return code;
        }
    }
}
