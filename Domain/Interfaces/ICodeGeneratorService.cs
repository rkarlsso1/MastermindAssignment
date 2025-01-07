namespace MastermindAssignment.Domain.Interfaces
{
    //interface for the code generator service
    //used to generate the secret code
    public interface ICodeGeneratorService
    {
        List<int> GenerateCode(int length, int minValue, int maxValue);
    }
}