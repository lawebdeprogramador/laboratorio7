using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PhoneApp
{
    public class PhoneTranslator
    {
        string Letters = "ABCDEFGHYJQLMNOPQRSTUVWXYZ";
        string Numbers = "22233344455566677778889999";
        public string ToNumber(string alfanumericPhoneNumber)
        {
            var NumericPhoneNumber = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(alfanumericPhoneNumber))
            {
                alfanumericPhoneNumber = alfanumericPhoneNumber.ToUpper();
                foreach (var c in alfanumericPhoneNumber)
                {
                    if ("0123456789".Contains(c))
                    {
                        NumericPhoneNumber.Append(c);
                    }
                    else
                    {
                        var index = Letters.IndexOf(c);
                        if (index >= 0)
                        {
                            NumericPhoneNumber.Append(Numbers[index]);
                        }

                    }
                }

            }
            return NumericPhoneNumber.ToString();
        }
    
    }
}