using CommunAxiom.Transformations.AppModel;
using CommunAxiom.Transformations.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace BusinessTests.UserTests
{
    [TestClass]
    public class UserValidatorTests
    {
        [TestMethod]
        [DataRow(OperationType.CREATE, 0, "best1@pirates.carib", "Jack Sparrow", true, "", DisplayName = "Create succeeds")]
        [DataRow(OperationType.CREATE, 1, null, "Jack Sparrow", false, ERROR_CODES.MANDATORY, DisplayName = "Create fails (Username mandatory)")]
        [DataRow(OperationType.CREATE, 1, "be", "Jack Sparrow", false, ERROR_CODES.MIN_LEN, DisplayName = "Create fails (Username min length)")]
        [DataRow(OperationType.CREATE, 1, "best@pirates.carib", "Jack Sparrow", false, ERROR_CODES.UK_EXISTS, DisplayName = "Create fails (Username already exists)")]
        [DataRow(OperationType.CREATE, 1, "best@pirat%$`{}[]es.carib", "Jack Sparrow", false, ERROR_CODES.REGEX, DisplayName = "Update fails (Username format)")]
        [DataRow(OperationType.CREATE, 0, "best1@pirates.carib", null, false, ERROR_CODES.MANDATORY, DisplayName = "Create fails (Name mandatory")]
        [DataRow(OperationType.CREATE, 0, "be", null, false, ERROR_CODES.MIN_LEN, DisplayName = "Create fails (Name min len")]
        public void TestValidations(OperationType operationType, int id, string username, string name, bool succeeds, string expectedCode)
        {
            var oa = Setup.ServiceProvider.GetService<IOperationAccessor>();

            oa.SetCurrentOperation(operationType);
            User m = new User
            {
                Id = id,
                Username = username,
                Name = name,
                LastConnected = DateTime.UtcNow
            };

            var validator = Setup.ServiceProvider.GetRequiredService<IValidator<User>>();
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