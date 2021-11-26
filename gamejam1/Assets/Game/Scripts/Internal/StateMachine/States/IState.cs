namespace SpellcastStudios
{
    public interface IState
    {
        StateManager manager { get; set; }

        //Runs once when entering new state
        void Enter();

        //Runs continuously in the update loop to receive input from external sources
        void Input();

        //Runs continuously in the fixed update to execute physics.
        void Action();

        //Runs once when exiting to a new state
        void Exit();
    }
}