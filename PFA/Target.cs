using System;
using System.Collections.Generic;
using System.Text;


    class Target
    {
        public string targetName;
        public float moneyAmount;
        DateTime deadline = new DateTime();


        public Target (string targetName, float moneyAmount, DateTime deadline)
        {
            this.targetName = targetName;
            this.moneyAmount = moneyAmount;
            this.deadline = deadline;
        }

        public static void ChangeMoneyAmount(Target target, float moneyAmount)
        {
            target.moneyAmount = moneyAmount;
        }

        public static void ChangeTargetName(Target target, string targetName)
        {
            target.targetName = targetName;
        }

        public static void ChangeDeadline(Target target, DateTime deadline)
        {
            target.deadline = deadline;
        }

        public static void AddMoney(Target target, float moneyAmount)
        {
            target.moneyAmount =+ moneyAmount;
        }

        public static float GetMoney (Target target)
        {
            return target.moneyAmount;
        }


    }

