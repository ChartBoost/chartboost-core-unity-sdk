using System;
using System.Collections;
using System.Threading.Tasks;
using Chartboost.Core.Consent;
using Newtonsoft.Json;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Chartboost.Core.Tests
{
    public class ConsentTests
    {
        [SetUp]
        public void SetUp()
        {
            ChartboostCore.Debug = true;
        }
        
        [Test, Order(2)]
        public void GetConsentStatus()
        {
            var status = ChartboostCore.Consent.ConsentStatus;
            ChartboostCoreLogger.Log($"ConsentStatus: {Enum.GetName(typeof(ConsentStatus), status)}");
            Assert.IsNotNull(status);
        }

        [Test, Order(3)]
        public void GetConsents()
        {
            var contents = ChartboostCore.Consent.Consents;
            Assert.IsNotNull(contents);
            var asJson = JsonConvert.SerializeObject(contents);
            ChartboostCoreLogger.Log($"Consents as Json: {asJson}");
            Assert.IsNotNull(asJson);
            Assert.IsNotEmpty(asJson);
        }

        [UnityTest, Order(2)]
        public IEnumerator GrantConsentDeveloper()
        {
            yield return TestConsent(ChartboostCore.Consent.GrantConsent, ConsentStatusSource.Developer);
        }
        
        [UnityTest, Order(2)]
        public IEnumerator DenyConsentDeveloper()
        {
            yield return TestConsent(ChartboostCore.Consent.DenyConsent, ConsentStatusSource.Developer);
        }
        
        
        [UnityTest, Order(2)]
        public IEnumerator GrantConsentUser()
        {
            yield return TestConsent(ChartboostCore.Consent.GrantConsent, ConsentStatusSource.User);
        }
        
        [UnityTest, Order(2)]
        public IEnumerator DenyConsentUser()
        {
            yield return TestConsent(ChartboostCore.Consent.DenyConsent, ConsentStatusSource.User);
        }
        
        [UnityTest, Order(2)]
        public IEnumerator ResetConsent()
        {
           var result = ChartboostCore.Consent.ResetConsent();
           yield return new WaitUntil(() => result.IsCompleted);
           Assert.IsFalse(result.Result);
        }

        private IEnumerator TestConsent(Func<ConsentStatusSource, Task<bool>> func, ConsentStatusSource source)
        {
            var task = func(source);
            yield return new WaitUntil(() => task.IsCompleted);
            Assert.IsFalse(task.Result);
        }
    }
}
