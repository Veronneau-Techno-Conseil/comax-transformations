using CommunAxiom.Transformations.AppModel;
using CommunAxiom.Transformations.Contracts;
using CommunAxiom.Transformations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace BusinessTests.ModuleTests
{
    [TestClass]
    public class ModuleValidatorTests
    {
        [TestMethod]
        [DataRow(OperationType.CREATE, 0, "module", "best@pirates.carib", ModuleType.PYTHON, true, "", DisplayName = "Create succeeds")]
        [DataRow(OperationType.UPDATE, 1, "module", "best@pirates.carib", ModuleType.PYTHON, true, "", DisplayName = "Update succeeds")]
        [DataRow(OperationType.UPDATE, 0, "module", "best@pirates.carib", ModuleType.PYTHON, false, ERROR_CODES.NOT_FOUND, DisplayName = "Update fails (Not found)")]
        [DataRow(OperationType.UPDATE, 1, null, "best@pirates.carib", ModuleType.PYTHON, false, ERROR_CODES.MANDATORY, DisplayName = "Update fails (Code mandatory)")]
        [DataRow(OperationType.UPDATE, 1, "mo", "best@pirates.carib", ModuleType.PYTHON, false, ERROR_CODES.MIN_LEN, DisplayName = "Update fails (Code min length)")]
        [DataRow(OperationType.UPDATE, 1, "mo@#test=%", "best@pirates.carib", ModuleType.PYTHON, false, ERROR_CODES.REGEX, DisplayName = "Update fails (Code format)")]
        [DataRow(OperationType.UPDATE, 1, "module", null, ModuleType.PYTHON, false, ERROR_CODES.MANDATORY, DisplayName = "Update fails (Creator mandatory)")]
        [DataRow(OperationType.UPDATE, 1, "module", "bst@pirates.carib", ModuleType.PYTHON, false, ERROR_CODES.FK, DisplayName = "Update fails (Creator FK)")]
        [DataRow(OperationType.UPDATE, 1, "module", "best@pirates.carib", null, false, ERROR_CODES.MANDATORY, DisplayName = "Update fails (ModuleType Mandatory)")]
        [DataRow(OperationType.UPDATE, 1, "module", "best@pirates.carib", "doesn't exist", false, ERROR_CODES.FK, DisplayName = "Update fails (ModuleType FK)")]
        public void TestValidations(OperationType operationType, int id, string code, string creator, string moduleType, bool succeeds, string expectedCode)
        {
            var oa = Setup.ServiceProvider.GetService<IOperationAccessor>();
            
            oa.SetCurrentOperation(operationType);
            Module m = new Module
            {
                Id = id,
                Code = code,
                Creator = creator,
                ModuleTypeCode = moduleType
            };

            var validator = Setup.ServiceProvider.GetRequiredService<IValidator<Module>>();
            var res = validator.Validate(m);

            if (succeeds)
            {
                Assert.IsTrue(res.IsValid);
                Assert.IsTrue(res.Errors.Count == 0, "Error count should be 0 for successful validation");
            }
            else
            {
                Assert.IsFalse(res.IsValid);
                Assert.IsTrue(res.Errors.Exists(x => x.ErrorCode == expectedCode));
            }
        }
    }
}
