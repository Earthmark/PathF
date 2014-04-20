using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Path
{
	/// <summary>
	/// Basic interface for accessing a stat.
	/// </summary>
	public interface IStat
	{
		float Value { get; }
	}

	/// <summary>
	/// Defines a stat that is independent on other values.
	/// </summary>
	public sealed class StaticStat : IStat
	{
		public float Value { get; set; }

		public StaticStat(float value = 0f)
		{
			Value = value;
		}
	}

	public enum StaticStats
	{
		StrengthScore,
		StrengthTempAdjustment,
		StrengthTempModifier,

		DexterityScore,
		DexterityTempAdjustment,
		DexterityTempModifier,

		ConstitutionScore,
		ConstitutionTempAdjustment,
		ConstitutionTempModifier,

		IntelligenceScore,
		IntelligenceTempAdjustment,
		IntelligenceTempModifier,

		WisdomScore,
		WisdomTempAdjustment,
		WisdomTempModifier,

		ChrismaScore,
		ChrismaTempAdjustment,
		ChrismaTempModifier,

		InitiativeMiscModifier,

		NaturalArmor,
		ArmorBonus,
		ShieldBonus,
		DeflectionModifier,
	}

	public delegate float Retriever(Character character);

	/// <summary>
	/// Defines a stat that is dependent on other values.
	/// </summary>
	public sealed class DynamicStat : IStat
	{
		private readonly Retriever retriever;
		private readonly Character character;

		public float Value
		{
			get { return retriever(character); }
		}

		public DynamicStat(Retriever retriever, Character character)
		{
			this.retriever = retriever;
			this.character = character;
		}
	}

	public static class DynamicMethods
	{
		#region Abilities

		private static float AbilityChange(float score)
		{
			var num = (score - 10.0) / 2.0;
			return (int)(num < 0.0 ? num - 1.0 : num);
		}

		[Export(typeof(Retriever))]
		public static float StrengthModifier(Character character)
		{
			var sta = character.Stats;
			var abi = AbilityChange(sta[StaticStats.StrengthScore] + sta[StaticStats.StrengthTempAdjustment]);
			return abi + sta[StaticStats.StrengthTempModifier];
		}

		[Export(typeof(Retriever))]
		public static float DexterityModifier(Character character)
		{
			var sta = character.Stats;
			var abi = AbilityChange(sta[StaticStats.DexterityScore] + sta[StaticStats.DexterityTempAdjustment]);
			return abi + sta[StaticStats.DexterityTempModifier];
		}

		[Export(typeof(Retriever))]
		public static float ConstitutionModifier(Character character)
		{
			var sta = character.Stats;
			var abi = AbilityChange(sta[StaticStats.ConstitutionScore] + sta[StaticStats.ConstitutionTempAdjustment]);
			return abi + sta[StaticStats.ConstitutionTempModifier];
		}

		[Export(typeof(Retriever))]
		public static float IntelligenceModifier(Character character)
		{
			var sta = character.Stats;
			var abi = AbilityChange(sta[StaticStats.IntelligenceScore] + sta[StaticStats.IntelligenceTempAdjustment]);
			return abi + sta[StaticStats.IntelligenceTempModifier];
		}

		[Export(typeof(Retriever))]
		public static float WisdomModifier(Character character)
		{
			var sta = character.Stats;
			var abi = AbilityChange(sta[StaticStats.WisdomScore] + sta[StaticStats.WisdomTempAdjustment]);
			return abi + sta[StaticStats.WisdomTempModifier];
		}

		[Export(typeof(Retriever))]
		public static float ChrismaModifier(Character character)
		{
			var sta = character.Stats;
			var abi = AbilityChange(sta[StaticStats.ChrismaScore] + sta[StaticStats.ChrismaTempAdjustment]);
			return abi + sta[StaticStats.ChrismaTempModifier];
		}

		#endregion

		[Export(typeof(Retriever))]
		public static float Initiative(Character character)
		{
			var sta = character.Stats;
			return sta[StaticStats.InitiativeMiscModifier] + sta[DexterityModifier];
		}

		[Export(typeof(Retriever))]
		public static float BaseAttackBonus(Character character)
		{
			throw new NotImplementedException();
		}

		[Export(typeof(Retriever))]
		public static float CombatManueverBonus(Character character)
		{
			var sta = character.Stats;
			return sta[BaseAttackBonus] + sta[StrengthModifier];
			//Add size modifier.
		}

		[Export(typeof(Retriever))]
		public static float CombatManueverDefense(Character character)
		{
			var sta = character.Stats;
			return sta[BaseAttackBonus] + sta[DexterityModifier] +
				sta[StrengthModifier] + 10;
			//Add size modifier.
		}
	}
}
