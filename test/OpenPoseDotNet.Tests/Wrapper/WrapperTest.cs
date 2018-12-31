﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OpenPoseDotNet.Tests.Wrapper
{

    [TestClass]
    public class WrapperTest
    {

        [TestMethod]
        public void Create()
        {
            using (var _ = new OpenPoseDotNet.Wrapper<Datum>())
                Console.WriteLine($"{nameof(OpenPoseDotNet.Wrapper<Datum>)} was created.");
        }

        [TestMethod]
        public void Destroy()
        {
            foreach (var mode in Enum.GetValues(typeof(ThreadManagerMode)).Cast<ThreadManagerMode>())
            {
                var wrapper = new OpenPoseDotNet.Wrapper<Datum>(mode);
                wrapper.Dispose();
            }
        }

    }

}
