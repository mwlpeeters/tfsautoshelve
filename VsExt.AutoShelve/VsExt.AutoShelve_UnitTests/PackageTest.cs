﻿/***************************************************************************
Copyright (c) Microsoft Corporation. All rights reserved.
This code is licensed under the Visual Studio SDK license terms.
THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
***************************************************************************/
using Microsoft.VsSDK.UnitTestLibrary;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VsExt.AutoShelve;

namespace VsExt.AutoShelve_UnitTests {
    [TestClass]
    public class PackageTest {
        [TestMethod]
        public void CreateInstance() {
            var package = new VsExtAutoShelvePackage();
            Assert.IsNotNull(package);
        }

        [TestMethod]
        public void IsIVsPackage() {
            var package = new VsExtAutoShelvePackage();
// ReSharper disable once RedundantCast
            Assert.IsNotNull(package as IVsPackage, "The object does not implement IVsPackage");
        }

        [TestMethod]
        public void SetSite() {
            // Create the package
            var package = new VsExtAutoShelvePackage() as IVsPackage;
            Assert.IsNotNull(package, "The object does not implement IVsPackage");

            // Create a basic service provider
            var serviceProvider = OleServiceProvider.CreateOleServiceProviderWithBasicServices();

            var activityLogMock = new GenericMockFactory("MockVsActivityLog", new[] { typeof(Microsoft.VisualStudio.Shell.Interop.IVsActivityLog) }).GetInstance();

            serviceProvider.AddService(typeof (Microsoft.VisualStudio.Shell.Interop.SVsActivityLog), activityLogMock, true);

            // Site the package
            Assert.AreEqual(0, package.SetSite(serviceProvider), "SetSite did not return S_OK");

            // Unsite the package
            Assert.AreEqual(0, package.SetSite(null), "SetSite(null) did not return S_OK");
        }

    }
}