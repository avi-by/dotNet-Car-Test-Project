using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class Test :ICloneable
    {
        private string id;
        private string testerId;
        private string traineeId;
        private DateTime date;
        private Address address;
        private bool signals;
        private bool keepDistance;
        private bool mirrorCheck;
        private bool test4;
        private bool test5;
        //...
        private bool succeeded;
        private string notes;

        public Test(string id, string testerId, string traineeId, DateTime date, Address address, bool test1, bool test2, bool test3, bool test4, bool test5, bool succeeded, string notes)
        {
            this.id = id;
            this.testerId = testerId;
            this.traineeId = traineeId;
            this.date = date;
            this.address = address;
            this.signals = test1;
            this.keepDistance = test2;
            this.mirrorCheck = test3;
            this.test4 = test4;
            this.test5 = test5;
            this.succeeded = succeeded;
            this.notes = notes;
        }

        public string Id { get => id; set => id = value; }
        public string TesterId { get => testerId; set => testerId = value; }
        public string TraineeId { get => traineeId; set => traineeId = value; }
        public DateTime Date { get => date; set => date = value; }
        public Address Address { get => address; set => address = value; }
        public bool Signals { get => signals; set => signals = value; }
        public bool KeepDistance { get => keepDistance; set => keepDistance = value; }
        public bool MirrorCheck { get => mirrorCheck; set => mirrorCheck = value; }
        public bool Test4 { get => test4; set => test4 = value; }
        public bool Test5 { get => test5; set => test5 = value; }
        public bool Succeeded { get => succeeded; set => succeeded = value; }
        public string Notes { get => notes; set => notes = value; }

        public object Clone()
        {
            Test temp = (Test)MemberwiseClone();
            temp.Address = new Address(address.street, address.houseNumber, address.city);
            temp.Date = new DateTime(Date.Year, Date.Month, Date.Day);
            return temp;
        }

        public override string ToString()
        {
            return Id+" tester id: "+TesterId+" treainee id: "+TraineeId+" ";
        }
    }
}
