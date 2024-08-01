using System;
using System.Collections;
using System.Threading.Tasks;
using Chartboost.Core.Consent;
using Chartboost.Json;
using Chartboost.Logging;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Chartboost.Core.Tests
{
    public class ConsentTests
    {
        [SetUp]
        public void SetUp() => ChartboostCore.LogLevel = LogLevel.Verbose;

        [Test, Order(3)]
        public void Consents()
        {
            var contents = ChartboostCore.Consent.Consents;
            Assert.IsNotNull(contents);
            var asJson = JsonTools.SerializeObject(contents);
            LogController.Log($"Consents as Json: {asJson}", LogLevel.Debug);
            Assert.IsNotNull(asJson);
            Assert.IsNotEmpty(asJson);
        }

        [UnityTest, Order(2)]
        public IEnumerator GrantConsentDeveloper()
        {
            yield return TestConsent(ChartboostCore.Consent.GrantConsent, ConsentSource.Developer);
        }
        
        [UnityTest, Order(2)]
        public IEnumerator DenyConsentDeveloper()
        {
            yield return TestConsent(ChartboostCore.Consent.DenyConsent, ConsentSource.Developer);
        }
        
        
        [UnityTest, Order(2)]
        public IEnumerator GrantConsentUser()
        {
            yield return TestConsent(ChartboostCore.Consent.GrantConsent, ConsentSource.User);
        }
        
        [UnityTest, Order(2)]
        public IEnumerator DenyConsentUser()
        {
            yield return TestConsent(ChartboostCore.Consent.DenyConsent, ConsentSource.User);
        }
        
        [UnityTest, Order(2)]
        public IEnumerator ResetConsent()
        {
           var result = ChartboostCore.Consent.ResetConsent();
           yield return new WaitUntil(() => result.IsCompleted);
           Assert.IsNotNull(result.Result);
        }

        private static IEnumerator TestConsent(Func<ConsentSource, Task<bool>> func, ConsentSource source)
        {
            var task = func(source);
            yield return new WaitUntil(() => task.IsCompleted);
            Assert.IsNotNull(task.Result);
        }
    }
}
