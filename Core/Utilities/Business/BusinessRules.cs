using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Business
{
    public class BusinessRules
    {
        public static IResult Run(params IResult[] logics) // istediğimiz kadar değer gönderebilelim diye
        {
            foreach (var logic in logics)
            {
                if(!logic.Success)
                {
                    return logic;
                }
            }

            return null;
        }
    }
}
