using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Path
{
    public sealed class Stats
    {
    }

    public abstract class Stat
    {
        protected Stats stats;

        public abstract int Score { get; }

        protected Stat(Stats stats)
        {
            this.stats = stats;
        }
    }

    #region Abilities

    public abstract class Ability : Stat
    {
        public override int Score
        {
            get { return BaseScore + TempAdjustment; }
        }

        public int Modifier
        {
            get
            {
                var num = (Score - 10.0) / 2.0;
                return (int) (num < 0.0 ? num - 1.0 : num) + TempModifier;
            }
        }

        public int BaseScore { get; private set; }

        public int TempAdjustment { get; set; }

        public int TempModifier { get; set; }

        public Ability(Stats stats, int baseScore)
            : base(stats)
        {
            BaseScore = baseScore;
        }
    }

    public sealed class Strength : Ability
    {
        public Strength(Stats stats, int baseScore) : base(stats, baseScore) { }
    }

    public sealed class Dexterity : Ability
    {
        public Dexterity(Stats stats, int baseScore) : base(stats, baseScore) { }
    }

    public sealed class Constitution : Ability
    {
        public Constitution(Stats stats, int baseScore) : base(stats, baseScore) { }
    }

    public sealed class Intelligence : Ability
    {
        public Intelligence(Stats stats, int baseScore) : base(stats, baseScore) { }
    }

    public sealed class Wisdom : Ability
    {
        public Wisdom(Stats stats, int baseScore) : base(stats, baseScore) { }
    }

    public sealed class Chrisma : Ability
    {
        public Chrisma(Stats stats, int baseScore) : base(stats, baseScore) { }
    }

    #endregion
}
