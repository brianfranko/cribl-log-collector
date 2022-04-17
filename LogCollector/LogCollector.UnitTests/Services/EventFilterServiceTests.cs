using System.Collections.Generic;
using System.Linq;
using LogCollector.Domain;
using LogCollector.Services;
using NUnit.Framework;

namespace LogCollector.UnitTests.Services
{
    [TestFixture]
    public class EventFilterServiceTests
    {
       private IEnumerable<Event> events = new List<Event>()
       {
           new Event()
           {
               timestamp = "Jun 17 20:55:06",
               Message = "combo ftpd[30753]: connection from 82.252.162.81 (lns-vlq-45-tou-82-252-162-81.adsl.proxad.net) at Fri Jun 17 20:55:06 2005"
           },
           new Event()
           {
               timestamp = "Jun 17 20:55:06",
               Message = "combo ftpd[30754]: connection from 82.252.162.81 (lns-vlq-45-tou-82-252-162-81.adsl.proxad.net) at Fri Jun 17 20:55:06 2005"
           },
           new Event()
           {
               timestamp = "Jun 17 20:55:06",
               Message = "combo ftpd[30755]: connection from 82.252.162.81 (lns-vlq-45-tou-82-252-162-81.adsl.proxad.net) at Fri Jun 17 20:55:06 2005"
           }
       };
        
        [Test]
        public void EventFilterServiceReturnsTheCorrectAmountOfEvents()
        {
            var eventFilterService = new EventFilterService();
            var actual = eventFilterService.FilterNumberOfEvents(events, 1);
            Assert.AreEqual(actual.Count(), 1);
        }
    }
}