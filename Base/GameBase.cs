namespace MastermindAssignment.Base
{
    //base class for the game
    //this class is abstract and has abstract methods to initialize, play and end the game
    public abstract class GameBase
    {
        public abstract void Initialize();      //abstract method to initialize the game

        public abstract void Play();            //abstract method to play the game

        public abstract void End();             //abstract method to end the game
    }
}