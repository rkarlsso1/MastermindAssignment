namespace MastermindAssignment.Domain.Models
{
    //class to store the game settings
    public class GameSettings
    {
        public int DigitsCount { get; }     //number of digits in the code
        public int MaxAttempts { get; }     //maximum number of attempts
        public int MinDigitValue { get; }   //minimum digit value
        public int MaxDigitValue { get; }   //maximum digit value

        public GameSettings(int digitsCount, int maxAttempts, int minDigitValue, int maxDigitValue)
        {
            DigitsCount = digitsCount;
            MaxAttempts = maxAttempts;
            MinDigitValue = minDigitValue;
            MaxDigitValue = maxDigitValue;
        }
    }
}
