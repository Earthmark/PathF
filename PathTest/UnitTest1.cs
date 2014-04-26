using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Path;

namespace PathTest
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			var character = new Character();

		    var stat = character.Stats[DynamicMethods.ArmorClass];
		    
		}
	}
}
