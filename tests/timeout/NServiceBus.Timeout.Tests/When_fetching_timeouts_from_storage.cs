namespace NServiceBus.Timeout.Tests
{
    using System;
    using System.Linq;
    using Core;
    using NUnit.Framework;

    [TestFixture]
    public class When_fetching_timeouts_from_storage : WithRavenTimeoutPersister
    {
        const int TimeoutsToAdd = 10;

        [Test]
        public void Should_return_the_complete_list_of_timeouts_that_are_due_within_2_hours()
        {
            for (var i = 0; i < TimeoutsToAdd; i++)
            {
                persister.Add(new TimeoutData
                {
                    Time = DateTime.UtcNow.AddHours(1)
                });
            }

            Assert.AreEqual(TimeoutsToAdd, persister.GetAll().Count());
        }

        [Test]
        public void Should_not_return_timeouts_that_are_not_due_within_2_hours()
        {
            for (var i = 0; i < TimeoutsToAdd; i++)
            {
                persister.Add(new TimeoutData
                                  {
                                      Time = DateTime.UtcNow.AddHours(3)
                                  });
                persister.Add(new TimeoutData
                                  {
                                      Time = DateTime.UtcNow.AddHours(1)
                                  });

            }

            Assert.AreEqual(TimeoutsToAdd, persister.GetAll().Count());
        }
    }
}