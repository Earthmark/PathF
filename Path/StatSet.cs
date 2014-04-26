using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;

namespace Path
{
	public sealed class StatSet
	{
		private readonly Dictionary<string, IStat> stats;

		public float this[string str]
		{
			get
			{
				IStat stat;
				return stats.TryGetValue(str, out stat) ?
					stat.Value : 0f;
			}
			set
			{
				IStat stat;
				if(stats.TryGetValue(str, out stat))
				{
					var staticStat = stat as StaticStat;
					if(staticStat != null)
					{
						staticStat.Value = value;
					}
				}
				else
				{
					stats[str] = new StaticStat(value);
				}
			}
		}

		public float this[StaticStats stat]
		{
			get { return this[stat.ToString()]; }
			set { this[stat.ToString()] = value; }
		}

		/// <summary>
		/// This method should be used in conjunction with <see cref="DynamicMethods"/>
		/// methods.
		/// </summary>
		/// <param name="retriever"></param>
		/// <returns></returns>
		public float this[Retriever retriever]
		{
			get { return this[retriever.Method.Name]; }
		}

		public StatSet(Character character)
		{
			stats = new Dictionary<string, IStat>();
			using(var container = new CompositionContainer(new AssemblyCatalog(typeof(StatSet).Assembly)))
			{
				foreach(var val in container.GetExports<Retriever>())
				{
					var method = val.Value;
					var name = method.Method.Name;
					var stat = new DynamicStat(method, character);
					stats[name] = stat;
				}
			}
		}
	}
}
