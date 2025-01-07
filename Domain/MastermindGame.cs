using MastermindAssignment.Base;
using MastermindAssignment.Domain.Interfaces;
using MastermindAssignment.Domain.Models;

namespace MastermindAssignment.Domain
{
    //Mastermind game class
    public class MastermindGame : GameBase
    {
        private readonly ICodeGeneratorService _codeGenerator;
        private readonly IGuessEvaluatorService _guessEvaluator;
        private readonly GameSettings _settings;

        private List<int>? _secretCode;

        public MastermindGame(ICodeGeneratorService codeGenerator, IGuessEvaluatorService guessEvaluator, GameSettings settings)
        {
            _codeGenerator = codeGenerator;
            _guessEvaluator = guessEvaluator;
            _settings = settings;
        }

        //initialize the game
        public override void Initialize()
        {
            _secretCode = _codeGenerator.GenerateCode(
            _settings.DigitsCount,
            _settings.MinDigitValue,
            _settings.MaxDigitValue
        );
        }

        //play the game
        public override void Play()
        {
            //safety check. if someone calls Play() before Initialize(), throw an error
            if (_secretCode == null)
            {
                throw new InvalidOperationException("Secret code is not yet initialized. Call Initialize() first.");
            }

            //track whether the user wins
            bool isWinner = false;

            //game text to console
            Console.WriteLine("Welcome to Mastermind!\n");
            Console.WriteLine($"You have {_settings.MaxAttempts} attempts to guess the {_settings.DigitsCount}-digit secret code " +
                              $"(digits range {_settings.MinDigitValue} to {_settings.MaxDigitValue}).\n");
            Console.WriteLine("Hint symbols:");
            Console.WriteLine("  '+' => correct digit & correct position");
            Console.WriteLine("  '-' => correct digit & wrong position\n");

            //main game loop
            for (int attempt = 1; attempt <= _settings.MaxAttempts; attempt++)
            {
                Console.Write($"Attempt {attempt}/{_settings.MaxAttempts} - Enter your guess: ");
                
                string input = Console.ReadLine() ?? string.Empty;

                // validate the input length
                if (input.Length != _settings.DigitsCount)
                {
                    Console.WriteLine($"Invalid guess length. Please enter exactly {_settings.DigitsCount} digits.\n");
                    attempt--;  // do not increase the attempt count if the input is invalid
                    continue;
                }

                //parse the input into a list of integers
                List<int> guessList;

                try
                {
                    guessList = input
                        .Select(ch => int.Parse(ch.ToString())) //could throw FormatException
                        .ToList();
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid characters in your guess. Only digits are allowed.\n");

                    attempt--;

                    continue;
                }

                //validate the input range (1-6 by default)
                if (guessList.Any(d => d < _settings.MinDigitValue || d > _settings.MaxDigitValue))
                {
                    Console.WriteLine($"All digits must be between {_settings.MinDigitValue} and {_settings.MaxDigitValue}.\n");
                    
                    attempt--;
                    
                    continue;
                }

                //evaluate the guess
                string hint = _guessEvaluator.EvaluateGuess(guessList, _secretCode);

                Console.WriteLine($"Hint: {hint}\n");

                //check if the player has won
                if (IsWinningHint(hint))
                {
                    isWinner = true;

                    break; 
                }
            }

            //print the secret code 
            Console.WriteLine($"The secret code was: {string.Join("", _secretCode)}\n");

            //decide outcome
            if (isWinner)
            {
                Console.WriteLine("Congratulations! You cracked the code!\n");
            }
            else
            {
                Console.WriteLine("Sorry, you've used all your attempts. Game over.\n");
            }       
        }

        //end the game
        public override void End()
        {
            Console.WriteLine("Thank you for playing Mastermind!");
        }

        //helper method to check if the hint is a winning hint
        private bool IsWinningHint(string hint)
        {
            return hint.Length == _settings.DigitsCount && hint.All(ch => ch == '+');
        }
    }
}
