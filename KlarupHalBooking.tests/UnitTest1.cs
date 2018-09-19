using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KlarupHalBooking.tests
{
    [TestClass]
    public class EntitiesTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ThrowExceptionIfEndDateTimeIsLessThenStartDateTime()
        {
            //arange
            KlarupHalBooking.Entities.HallBooking TestBooking = new Entities.HallBooking();
            //act
            TestBooking.HallBookingTime = DateTime.Now;
            //assert/act
            TestBooking.HallBookingEndTime = DateTime.Now.AddDays(-2);


        }
    }
}
