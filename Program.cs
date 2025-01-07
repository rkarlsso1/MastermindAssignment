using MastermindAssignment.Base;
using MastermindAssignment.Domain.Interfaces;
using MastermindAssignment.Services.Generators;
using MastermindAssignment.Services.Evaluators;
using MastermindAssignment.Domain.Models;
using MastermindAssignment.Domain;

namespace MastermindAssignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                //create instances of the services
                ICodeGeneratorService codeGenerator = new RandomCodeGeneratorService();
                IGuessEvaluatorService guessEvaluator = new MastermindGuessEvaluatorService();

                //create a settings object
                var settings = new GameSettings(
                    digitsCount: 4,
                    maxAttempts: 10,
                    minDigitValue: 1,
                    maxDigitValue: 6
                );

                //pass the settings object to MastermindGame
                GameBase game = new MastermindGame(codeGenerator, guessEvaluator, settings);

                //run the game
                game.Initialize();
                game.Play();
                game.End();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An unexpected error occurred:");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }
        }
    }
}
