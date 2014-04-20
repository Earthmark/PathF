using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
