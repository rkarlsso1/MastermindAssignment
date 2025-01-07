namespace MastermindAssignment.Domain.Interfaces
{
    //interface for the guess evaluator service
    //used to evaluate the guess and return the hint
    public interface IGuessEvaluatorService
    {
        string EvaluateGuess(List<int> guess, List<int> secretCode);
    }
}