namespace HorseRaceSimulation
{
    internal class ProgressBar
    {
        public int Minimum { get; internal set; }
        public int Maximum { get; internal set; }
        public int Value { get; internal set; }
        public int Width { get; internal set; }
        public int Height { get; internal set; }
        public Padding Margin { get; internal set; }

        internal void Invoke(MethodInvoker methodInvoker)
        {
            throw new NotImplementedException();
        }
    }
}