using OPZ_Mekin;
using System;
using System.Collections.Generic;

namespace OPZWPF
{
    class StepX
    {
        public int Step { get; private set; }

        public int BeginInterval { get; private set; }

        public int EndInterval { get; private set; }

        public int CurrentX { get; private set; }

        public char[] Func { get; private set; }

        public static Dictionary<double, double> AllResult = new Dictionary<double, double>();

        public StepX(int step, int currentX, int begin,int end, char[] func)
        {
            Step = step;
            CurrentX = currentX;
            BeginInterval = begin;
            EndInterval = end;
            Func = func;
            AllResult.Clear();
        }



        public List<Resultes> GetTable()
        {
            var allResult = new List<Resultes>();
            var opz = new ReverseReader().Reverse(Func); ;
            for (int i = BeginInterval; i <= EndInterval; i++)
            {
                if (CurrentX > EndInterval)
                    break;
                var calc = new Calculation(CurrentX);
                var result = new Resultes(CurrentX, calc.Calculate(opz.ToArray()));
                allResult.Add(result);
                AllResult.Add(result.RezultX, result.RezultY);
                CurrentX += Step;
            }
            return allResult;
        }
    }
}
