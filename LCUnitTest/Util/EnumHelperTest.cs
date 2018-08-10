using System;
using LightCycleClone;
using LightCycleClone.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LCUnitTest.Util
{
    [TestClass]
    public class EnumHelperTest
    {
        [TestMethod]
        public void GetList()
        {
            var playerActions = EnumUtil.GetList<PlayerAction>();

            Assert.IsTrue(playerActions.Count > 0);
        }
    }
}
