using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Application.Helpers;
using System.Collections.Generic;
using System.Text;

namespace WishListTests.Helpers
{
    [TestClass]
    public class ConvertHttpHelperTest : WishListTests
    {
        [TestMethod]
        public void HttpConvert_Helper_SuccessTrue()
        {
            defaultResponse.success = true;

            var response = defaultResponse.Convert();

            int StatusCode = (int)response.GetType().GetProperty("StatusCode").GetValue(response);

            Assert.AreEqual(StatusCode, 200);
        }

        [TestMethod]
        public void HttpConvert_Helper_SuccessFalse()
        {
            defaultResponse.success = false;

            var response = defaultResponse.Convert();

            int StatusCode = (int)response.GetType().GetProperty("StatusCode").GetValue(response);

            Assert.AreEqual(StatusCode, 400);
        }
    }
}
