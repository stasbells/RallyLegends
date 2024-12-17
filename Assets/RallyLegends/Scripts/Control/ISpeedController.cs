namespace RallyLegends.Control
{
    public interface ISpeedController
    {
        public float MaxSpeed { get; }

        public float Change(float speed);
    }
}