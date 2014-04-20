//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Path
//{
//	#region Ability

//	public abstract class Ability : IStat
//	{
//		public int Score
//		{
//			get { return BaseScore + TempAdjustment; }
//		}

//		public int Modifier
//		{
//			get
//			{
//				var num = (Score - 10.0) / 2.0;
//				return (int)(num < 0.0 ? num - 1.0 : num) + TempModifier;
//			}
//		}

//		public int BaseScore { get; private set; }

//		public int TempAdjustment { get; set; }

//		public int TempModifier { get; set; }

//		public Ability(Character character, int baseScore)
//			: base(character)
//		{
//			BaseScore = baseScore;
//		}
//	}

//	public sealed class Strength : Ability
//	{
//		public Strength(Character character, int baseScore)
//			: base(character, baseScore) {}
//	}

//	public sealed class Dexterity : Ability
//	{
//		public Dexterity(Character character, int baseScore)
//			: base(character, baseScore) { }
//	}

//	public sealed class Constitution : Ability
//	{
//		public Constitution(Character character, int baseScore)
//			: base(character, baseScore) { }
//	}

//	public sealed class Intelligence : Ability
//	{
//		public Intelligence(Character character, int baseScore)
//			: base(character, baseScore) { }
//	}

//	public sealed class Wisdom : Ability
//	{
//		public Wisdom(Character character, int baseScore)
//			: base(character, baseScore) { }
//	}

//	public sealed class Chrisma : Ability
//	{
//		public Chrisma(Character character, int baseScore)
//			: base(character, baseScore) { }
//	}

//	#endregion

//	#region Other

//	public enum Sizes
//	{
//		//Value is the size modifier
//		Fine,
//		Diminutive,
//		Tiny,
//		Small,
//		Medium,
//		LargeTall,
//		LargeLong,
//		HugeTall,
//		HugeLong,
//		GargantuanTall,
//		GargantuanLong,
//		ColossalTall,
//		ColossalLong
//	}

//	public sealed class Size : IStat
//	{
//		private static readonly Dictionary<Sizes, int> modifiers;
//		private static readonly Dictionary<Sizes, int> flyModifiers;
//		private static readonly Dictionary<Sizes, float> spaceSizes;
//		private static readonly Dictionary<Sizes, int> reaches;

//		public Sizes CurrentSize { get; set; }

//		public int Modifier
//		{
//			get { return modifiers[CurrentSize]; }
//		}

//		public int SpecialSizeMofifier
//		{
//			get { return -Modifier; }
//		}

//		public int FlyModifier
//		{
//			get { return flyModifiers[CurrentSize]; }
//		}

//		public int StealthModifier
//		{
//			get { return FlyModifier * 2; }
//		}

//		public float Space
//		{
//			get { return spaceSizes[CurrentSize]; }
//		}

//		public float NaturalReach
//		{
//			get { return reaches[CurrentSize]; }
//		}

//		static Size()
//		{
//			modifiers = new Dictionary<Sizes, int>
//			{
//				{Sizes.Fine, 8},
//				{Sizes.Diminutive, 4},
//				{Sizes.Tiny, 2},
//				{Sizes.Small, 1},
//				{Sizes.Medium, 0},
//				{Sizes.LargeTall, -1},
//				{Sizes.LargeLong, -1},
//				{Sizes.HugeTall, -2},
//				{Sizes.HugeLong, -2},
//				{Sizes.GargantuanTall, -4},
//				{Sizes.GargantuanLong, -4},
//				{Sizes.ColossalTall, -8},
//				{Sizes.ColossalLong, -8}
//			};

//			flyModifiers = new Dictionary<Sizes, int>
//			{
//				{Sizes.Fine, 8},
//				{Sizes.Diminutive, 6},
//				{Sizes.Tiny, 4},
//				{Sizes.Small, 2},
//				{Sizes.Medium, 0},
//				{Sizes.LargeTall, -2},
//				{Sizes.LargeLong, -2},
//				{Sizes.HugeTall, -4},
//				{Sizes.HugeLong, -4},
//				{Sizes.GargantuanTall, -6},
//				{Sizes.GargantuanLong, -6},
//				{Sizes.ColossalTall, -8},
//				{Sizes.ColossalLong, -8}
//			};

//			spaceSizes = new Dictionary<Sizes, float>
//			{
//				{Sizes.Fine, 0.5f},
//				{Sizes.Diminutive, 1f},
//				{Sizes.Tiny, 2.5f},
//				{Sizes.Small, 5f},
//				{Sizes.Medium, 5f},
//				{Sizes.LargeTall, 10f},
//				{Sizes.LargeLong, 10f},
//				{Sizes.HugeTall, 15f},
//				{Sizes.HugeLong, 15f},
//				{Sizes.GargantuanTall, 20f},
//				{Sizes.GargantuanLong, 20f},
//				{Sizes.ColossalTall, 30f},
//				{Sizes.ColossalLong, 30f}
//			};

//			reaches = new Dictionary<Sizes, int>
//			{
//				{Sizes.Fine, 0},
//				{Sizes.Diminutive, 0},
//				{Sizes.Tiny, 0},
//				{Sizes.Small, 5},
//				{Sizes.Medium, 5},
//				{Sizes.LargeTall, 10},
//				{Sizes.LargeLong, 5},
//				{Sizes.HugeTall, 15},
//				{Sizes.HugeLong, 10},
//				{Sizes.GargantuanTall, 20},
//				{Sizes.GargantuanLong, 15},
//				{Sizes.ColossalTall, 30},
//				{Sizes.ColossalLong, 20}
//			};
//		}

//		public Size(Character character)
//			: base(character) { }
//	}

//	public sealed class Initiative : IStat
//	{
//		public int Score
//		{
//			get { return Character.Stats.Get<Dexterity>().Modifier + Misc; }
//		}

//		public int Misc { get; set; }

//		public Initiative(Character character)
//			: base(character) { }
//	}

//	public sealed class ArmorClass : IStat
//	{
//		public int Score
//		{
//			get
//			{
//				return ShieldBonus + ArmorBonus +
//					  Character.Stats.Get<Dexterity>().Modifier +
//					  Character.Stats.Get<Size>().Modifier +
//					  NaturalArmor + DeflectionModifier + MiscModifier +
//					  10;
//			}
//		}

//		public int ShieldBonus
//		{
//			get { throw new NotImplementedException(); }
//		}

//		public int ArmorBonus
//		{
//			get { throw new NotImplementedException("From items"); }
//		}

//		public int NaturalArmor
//		{
//			get { throw new NotImplementedException(); }
//		}

//		public int DeflectionModifier
//		{
//			get { throw new NotImplementedException();}
//		}

//		public int MiscModifier
//		{
//			get { throw new NotImplementedException();}
//		}

//		public ArmorClass(Character character)
//			: base(character) {}
//	}

//	public sealed class TouchArmorClass : IStat
//	{
//		public int Score
//		{
//			get
//			{
//				return Character.Stats.Get<Dexterity>().Modifier +
//					  Character.Stats.Get<Size>().Modifier +
//					  DeflectionModifier + MiscModifier +
//					  10;
//			}
//		}

//		public int DeflectionModifier
//		{
//			get { throw new NotImplementedException(); }
//		}

//		public int MiscModifier
//		{
//			get { throw new NotImplementedException(); }
//		}

//		public TouchArmorClass(Character character)
//			: base(character) { }
//	}

//	public sealed class FlatFootArmorClass : IStat
//	{
//		public int Score
//		{
//			get
//			{
//				return ShieldBonus + ArmorBonus +
//					  Character.Stats.Get<Size>().Modifier +
//					  NaturalArmor + DeflectionModifier + MiscModifier +
//					  10;
//			}
//		}

//		public int ShieldBonus
//		{
//			get { throw new NotImplementedException(); }
//		}

//		public int ArmorBonus
//		{
//			get { throw new NotImplementedException("From items"); }
//		}

//		public int NaturalArmor
//		{
//			get { throw new NotImplementedException(); }
//		}

//		public int DeflectionModifier
//		{
//			get { throw new NotImplementedException();}
//		}

//		public int MiscModifier
//		{
//			get { throw new NotImplementedException();}
//		}

//		public FlatFootArmorClass(Character character)
//			: base(character) {}
//	}

//	public sealed class BaseAttackBonus : IStat
//	{
//		public int MaxValue
//		{
//			get { return Values.Max(); }
//		} 

//		public IEnumerable<int> Values
//		{
//			get { throw new NotImplementedException("From class"); }
//		}

//		public BaseAttackBonus(Character character)
//			: base(character) { }
//	}

//	public sealed class CombatManueverBonus : IStat
//	{
//		public int Value
//		{
//			get
//			{
//				return Character.Stats.Get<BaseAttackBonus>().MaxValue +
//					  Character.Stats.Get<Strength>().Modifier +
//					  Character.Stats.Get<Size>().SpecialSizeMofifier;
//			}
//		}

//		public CombatManueverBonus(Character character)
//			: base(character) { }
//	}

//	public sealed class CombatManueverDefense : IStat
//	{
//		public int Value
//		{
//			get
//			{
//				return Character.Stats.Get<BaseAttackBonus>().MaxValue +
//					  Character.Stats.Get<Strength>().Modifier +
//					  Character.Stats.Get<Dexterity>().Modifier +
//					  Character.Stats.Get<Size>().SpecialSizeMofifier +
//					  10;
//			}
//		}

//		public CombatManueverDefense(Character character)
//			: base(character) { }
//	}

//	#endregion
//}
