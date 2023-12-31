﻿using System;

namespace Push.Acumatica.Api.Common
{
    public static class ValueExtensions
    {
        public static StringValue ToValue(this string input)
        {
            return new StringValue(input);
        }

        public static IntegerValue ToValue(this int input)
        {
            return new IntegerValue(input);
        }

        public static BoolValue ToValue(this bool input)
        {
            return new BoolValue(input);
        }

        public static DoubleValue ToValue(this double input)
        {
            return new DoubleValue(input);
        }

        public static DateValue ToValue(this DateTimeOffset input)
        {
            return new DateValue(input);
        }

        public static DateValue ToValue(this DateTime input)
        {
            return new DateValue(input);
        }
    }

    public class StringValue
    {
        public string value { get; set; }

        public StringValue()
        {

        }
        public StringValue(string _value)
        {
            value = _value;
        }

        public override string ToString()
        {
            return value;
        }

        public StringValue Copy()
        {
            return new StringValue(value);
        }
    }

    public class BoolValue
    {
        public bool value { get; set; }

        public BoolValue()
        {

        }
        public BoolValue(bool _value)
        {
            value = _value;
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }

    public class DateValue
    {
        public DateTimeOffset? value { get; set; }

        public DateValue()
        {
        }

        public DateValue(DateTimeOffset? _value)
        {
            value = _value;
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public DateValue Copy()
        {
            return new DateValue(value);
        }
    }

    public class IntegerValue
    {
        public int value { get; set; }

        public IntegerValue()
        {

        }
        public IntegerValue(int _value)
        {
            value = _value;
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }

    public class DoubleValue
    {
        public double value { get; set; }

        public DoubleValue()
        {
            value = 0;
        }

        public DoubleValue(double _value)
        {
            value = _value;
        }
        
        public DoubleValue Copy()
        {
            return new DoubleValue(value);
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
}
