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
        private bool test1_ReverseParking;
        private bool test2_KeepingSafeDistance;
        private bool test3_UsingMirrors;
        private bool test4_UsingTurnSignals;
        private bool test5_LegalSpeed;
        //...
        private bool succeeded;
        private string notes;

        public Test(string testerId, string traineeId, DateTime date, DateTime hour, Address address, bool test1_ReverseParking, bool test2_KeepingSafeDistance, bool test3_UsingMirrors, bool test4_UsingTurnSignals, bool test5_LegalSpeed, bool succeeded, string notes)
        {
            this.
            this.testerId = testerId;
            this.traineeId = traineeId;
            this.date = date;
            this.hour = hour;
            this.address = address;
            this.test1_ReverseParking = test1_ReverseParking;
            this.test2_KeepingSafeDistance = test2_KeepingSafeDistance;
            this.test3_UsingMirrors = test3_UsingMirrors;
            this.test4_UsingTurnSignals = test4_UsingTurnSignals;
            this.test5_LegalSpeed = test5_LegalSpeed;
            this.succeeded = succeeded;
            this.notes = notes;
        }


   
                public string Id { get => id; set => id = value; }
                public string TesterId { get => testerId; set => testerId = value; }
                public string TraineeId { get => traineeId; set => traineeId = value; }
                public DateTime Date { get => date; set => date = value; }
                public DateTime Hour { get => hour; set => hour = value; }
                public Address Address { get => address; set => address = value; }
                public bool Test1_ReverseParking { get => test1_ReverseParking; set => test1_ReverseParking = value; }
                public bool Test2_KeepingSafeDistance { get => test2_KeepingSafeDistance; set => test2_KeepingSafeDistance = value; }
                public bool Test3_UsingMirrors { get => test3_UsingMirrors; set => test3_UsingMirrors = value; }
                public bool Test4_UsingTurnSignals { get => test4_UsingTurnSignals; set => test4_UsingTurnSignals = value; }
                public bool Test5_LegalSpeed { get => test5_LegalSpeed; set => test5_LegalSpeed = value; }
                public bool Succeeded { get => succeeded; set => succeeded = value; }
                public string Notes { get => notes; set => notes = value; }


    public override string ToString()
        {
            return Id + " tester id: " + TesterId + " treainee id: " + TraineeId + " ";
        }

    }
}
