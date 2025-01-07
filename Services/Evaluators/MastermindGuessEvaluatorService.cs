using MastermindAssignment.Domain.Interfaces;

namespace MastermindAssignment.Services.Evaluators
{
    //service to evaluate the guess and return the hint
    public class MastermindGuessEvaluatorService : IGuessEvaluatorService
    {
        //evaluate the guess and return the hint
        public string EvaluateGuess(List<int> guess, List<int> secretCode)
        {
            //clone the lists so we dont mutate the originals
            var secretCopy = new List<int>(secretCode);
            var guessCopy = new List<int>(guess);

            int plusCount = 0; 
            int minusCount = 0; 

            //count + (correct digit in correct position)
            for (int i = 0; i < guessCopy.Count; i++)
            {
                //if the digit is correct
                if (guessCopy[i] == secretCopy[i])
                {
                    plusCount++;        //increment the count

                    guessCopy[i] = -2;  //mark the digit as correct in the guess

                    secretCopy[i] = -1; //mark the digit as correct in the secret code
                }
            }

            //count - (correct digit, wrong position)
            for (int i = 0; i < guessCopy.Count; i++)
            {
                //skip the ones that are already marked as correct
                if (guessCopy[i] == -2)

                    continue;

                //find the index of the current guess digit in the secret code
                int indexInSecret = secretCopy.IndexOf(guessCopy[i]);

                //if the digit is found in the secret code
                if (indexInSecret >= 0)
                {
                    minusCount++;                       //increment the count
                    
                    guessCopy[i] = -2;                  //mark the digit as correct in the guess

                    secretCopy[indexInSecret] = -1;     //mark the digit as correct in the secret code
                }
            }

            //construct the hint with all + first then -
            return new string('+', plusCount) + new string('-', minusCount);
        }
    }
}
