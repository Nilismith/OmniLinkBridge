﻿using OmniLinkBridge.MQTT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OmniLinkBridge
{
    public static class Extensions
    {
        public static double ToCelsius(this double f)
        {
            // Convert to celsius
            return (5.0 / 9.0 * (f - 32));
        }

        public static int ToOmniTemp(this double c)
        {
            // Convert to omni temp (0 is -40C and 255 is 87.5C)
            return (int)Math.Round((c + 40) * 2, 0);
        }

        public static bool IsBitSet(this byte b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }

        public static AreaCommandCode ToCommandCode(this string payload, bool supportValidate = false)
        {
            string[] payloads = payload.Split(',');
            int code = 0;

            AreaCommandCode ret = new AreaCommandCode()
            {
                Command = payloads[0]
            };

            if (payload.Length == 1)
                return ret;

            if (payloads.Length == 2)
            {
                ret.Success = int.TryParse(payloads[1], out code);
            }
            else if (supportValidate && payloads.Length == 3)
            {
                if (string.Compare(payloads[1], "validate", true) == 0)
                {
                    ret.Validate = true;
                    ret.Success = int.TryParse(payloads[2], out code);
                }
                else
                    ret.Success = false;
            }

            ret.Code = code;
            return ret;
        }

        public static string ToSpaceTitleCase(this string phrase)
        {
            return Regex.Replace(phrase, "(\\B[A-Z])", " $1");
        }

        public static List<int> ParseRanges(this string ranges)
        {
            string[] groups = ranges.Split(',');
            return groups.SelectMany(t => ParseRange(t)).ToList();
        }

        private static List<int> ParseRange(string range)
        {
            List<int> RangeNums = range
                .Split('-')
                .Select(t => new String(t.Where(Char.IsDigit).ToArray())) // Digits Only
                .Where(t => !string.IsNullOrWhiteSpace(t)) // Only if has a value
                .Select(t => int.Parse(t)).ToList(); // digit to int
            return RangeNums.Count.Equals(2) ? Enumerable.Range(RangeNums.Min(), (RangeNums.Max() + 1) - RangeNums.Min()).ToList() : RangeNums;
        }
    }
}
