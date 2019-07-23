using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace VowelCounterAWS
{
    public class Function
    {
        public static int FunctionHandler(string input, ILambdaContext context)
        {
            return CalculateVowel(input);
        }



        //Todo: Move to AWS
        public static int CalculateVowel(string phrase)
        {
            var vowelNum = 0;
            foreach (var character in phrase)
                foreach (char vowel in vowels)
                {
                    if (character == vowel)
                        vowelNum++;
                }

            return vowelNum;
        }
    }
}
