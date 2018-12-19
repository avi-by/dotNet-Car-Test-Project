using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class Test
    {
        private string id;
        private string testerId;
        private string traineeId;
        private DateTime date;
        private DateTime hour;
        private Address address;
        private bool test1;
        private bool test2;
        private bool test3;
        private bool test4;
        private bool test5;
        //...
        private bool succeeded;
        private string notes;

        public Test(string id, string testerId, string traineeId, DateTime date, DateTime hour, Address address, bool test1, bool test2, bool test3, bool test4, bool test5, bool succeeded, string notes)
        {
            this.id = id;
            this.testerId = testerId;
            this.traineeId = traineeId;
            this.date = date;
            this.hour = hour;
            this.address = address;
            this.test1 = test1;
            this.test2 = test2;
            this.test3 = test3;
            this.test4 = test4;
            this.test5 = test5;
            this.succeeded = succeeded;
            this.notes = notes;
        }

        public string Id { get => id; set => id = value; }
        public string TesterId { get => testerId; set => testerId = value; }
        public string TraineeId { get => traineeId; set => traineeId = value; }
        public DateTime Date { get => date; set => date = value; }
        public DateTime Hour { get => hour; set => hour = value; }
        public Address Address { get => address; set => address = value; }
        public bool Test1 { get => test1; set => test1 = value; }
        public bool Test2 { get => test2; set => test2 = value; }
        public bool Test3 { get => test3; set => test3 = value; }
        public bool Test4 { get => test4; set => test4 = value; }
        public bool Test5 { get => test5; set => test5 = value; }
        public bool Succeeded { get => succeeded; set => succeeded = value; }
        public string Notes { get => notes; set => notes = value; }

        public override string ToString()
        {
            return Id+" tester id: "+TesterId+" treainee id: "+TraineeId+" ";
        }
    }
}
