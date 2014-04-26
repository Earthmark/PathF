namespace Path
{
	public sealed class Character
	{
		public StatSet Stats { get; private set; }

		public Character()
		{
			Stats = new StatSet(this);
		}
	}
}
