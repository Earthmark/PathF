using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Path
{
	public enum SpecStat
	{
		ArmorBonus,
		ShieldBonus,
		NaturalArmor,
		DeflectionMod,

	}

	public sealed class Stats
	{
		private readonly Dictionary<Type, Stat> stats;
		private readonly Dictionary<SpecStat, int> specStats;

		public T Get<T>() where T : Stat
		{
			return (T)stats[typeof(T)];
		}

		public int Get(SpecStat stat)
		{
			int num;
			specStats.TryGetValue(stat, out num);
			return num;
		}

		public int Add(SpecStat stat, int num)
		{
			int cur;
			specStats.TryGetValue(stat, out cur);
			num += cur;
			specStats[stat] = num;
			return num;
		}

		public Stats()
		{
			using(var container = new CompositionContainer(new AssemblyCatalog(typeof(Stats).Assembly)))
			{
				var enumer = container.GetExportedValues<Stat>();

				stats = enumer.ToDictionary(k => k.GetType());
			}
			specStats = new Dictionary<SpecStat, int>();
		}
	}

	[InheritedExport]
	public abstract class Stat
	{
		protected readonly Character Character;

		protected Stat(Character character)
		{
			this.Character = character;
		}
	}
}
