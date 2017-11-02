using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeOkTemplateMethod.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeOkTemplateMethod.Models.Tests
{
    [TestClass()]
    public class CustomValidatorTests
    {
        [TestMethod()]
        public void IsValid_CorrectInput_Success()
        {
            string correctInput = "LolInputData";
            CustomValidator validator = new CustomValidator() { RequireStartsWithLol = true };

            Assert.IsTrue(validator.IsValid(correctInput));
        }

        [TestMethod()]
        public void IsValid_InorrectInput_Fail()
        {
            string correctInput = "NoLolInputData";
            CustomValidator validator = new CustomValidator() { RequireStartsWithLol = true };

            Assert.IsFalse(validator.IsValid(correctInput));
        }
    }
}