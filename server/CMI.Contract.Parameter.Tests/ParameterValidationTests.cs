using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CMI.Contract.Parameter.Tests
{
    [TestClass]
    public class ParameterValidationTests
    {
        [TestMethod]
        public void ParameterList_Should_be_valid()
        {
            var testSetting = new TestSetting
            {
                TestDate = "03.04.2018",
                TestFlag = true,
                TestMailAdress = "testmail@mail.ch",
                TestNumber = 20,
                TestMandatory = "Test"
            };
            var parameterList = ParameterHelper.GetParameterListFromSetting(testSetting);
            Assert.IsTrue(ParameterHelper.ValidateParameter(parameterList));
        }

        [TestMethod]
        public void Empty_value_and_mandatory_should_not_be_valid()
        {
            var testSetting = new TestSetting();
            var parameterList = ParameterHelper.GetParameterListFromSetting(testSetting);
            Assert.IsFalse(ParameterHelper.ValidateParameter(parameterList.First(p => p.Name.EndsWith("TestMandatory"))));
        }

        [TestMethod]
        public void None_empty_value_and_mandatory_should_be_valid()
        {
            var testSetting = new TestSetting();
            testSetting.TestMandatory = "Test";
            var parameterList = ParameterHelper.GetParameterListFromSetting(testSetting);
            Assert.IsTrue(ParameterHelper.ValidateParameter(parameterList.First(p => p.Name.EndsWith("TestMandatory"))));
        }

        [TestMethod]
        public void None_empty_value_that_is_not_conform_should_not_be_valide()
        {
            var testSetting = new TestSetting();
            testSetting.TestDate = "This is no Date!";
            var parameterList = ParameterHelper.GetParameterListFromSetting(testSetting);
            Assert.IsFalse(ParameterHelper.ValidateParameter(parameterList.First(p => p.Name.EndsWith("TestDate"))));
        }

        [TestMethod]
        public void None_empty_value_that_is_conform_should_be_valide()
        {
            var testSetting = new TestSetting();
            testSetting.TestDate = "03.04.2018";
            var parameterList = ParameterHelper.GetParameterListFromSetting(testSetting);
            Assert.IsTrue(ParameterHelper.ValidateParameter(parameterList.First(p => p.Name.EndsWith("TestDate"))));
        }

        [TestMethod]
        public void Empty_value_that_is_mandatory_and_has_a_regex_defined_should_not_be_valide()
        {
            var testSetting = new TestSetting();
            var parameterList = ParameterHelper.GetParameterListFromSetting(testSetting);
            Assert.IsFalse(ParameterHelper.ValidateParameter(parameterList.First(p => p.Name.EndsWith("TestMailAdress"))));
        }

        [TestMethod]
        public void None_empty_conform_value_that_is_mandatory_and_has_a_regex_defined_should_be_valide()
        {
            var testSetting = new TestSetting();
            testSetting.TestMailAdress = "max.muster@supermail.ch";
            var parameterList = ParameterHelper.GetParameterListFromSetting(testSetting);
            Assert.IsTrue(ParameterHelper.ValidateParameter(parameterList.First(p => p.Name.EndsWith("TestMailAdress"))));
        }

        [TestMethod]
        public void None_empty_none_conform_value_that_is_mandatory_and_has_a_regex_defined_should_not_be_valide()
        {
            var testSetting = new TestSetting();
            testSetting.TestMailAdress = "This is not a mail adress!";
            var parameterList = ParameterHelper.GetParameterListFromSetting(testSetting);
            Assert.IsFalse(ParameterHelper.ValidateParameter(parameterList.First(p => p.Name.EndsWith("TestMailAdress"))));
        }
    }
}
