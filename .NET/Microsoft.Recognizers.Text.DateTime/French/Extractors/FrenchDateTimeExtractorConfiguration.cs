﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Text.RegularExpressions;
using Microsoft.Recognizers.Definitions.French;
using Microsoft.Recognizers.Text.DateTime.French.Utilities;
using Microsoft.Recognizers.Text.DateTime.Utilities;
using Microsoft.Recognizers.Text.Number;

namespace Microsoft.Recognizers.Text.DateTime.French
{
    public class FrenchDateTimeExtractorConfiguration : BaseDateTimeOptionsConfiguration, IDateTimeExtractorConfiguration
    {
        // à - time at which, en - length of time, dans - amount of time
        public static readonly Regex PrepositionRegex =
          new Regex(DateTimeDefinitions.PrepositionRegex, RegexFlags);

        // right now, as soon as possible, recently, previously
        public static readonly Regex NowRegex =
            new Regex(DateTimeDefinitions.NowRegex, RegexFlags);

        // in the evening, afternoon, morning, night
        public static readonly Regex SuffixRegex =
            new Regex(DateTimeDefinitions.SuffixRegex, RegexFlags);

        public static readonly Regex TimeOfDayRegex =
            new Regex(DateTimeDefinitions.TimeOfDayRegex, RegexFlags);

        public static readonly Regex SpecificTimeOfDayRegex =
            new Regex(DateTimeDefinitions.SpecificTimeOfDayRegex, RegexFlags);

        public static readonly Regex TimeOfTodayAfterRegex =
             new Regex(DateTimeDefinitions.TimeOfTodayAfterRegex, RegexFlags);

        public static readonly Regex TimeOfTodayBeforeRegex =
            new Regex(DateTimeDefinitions.TimeOfTodayBeforeRegex, RegexFlags);

        public static readonly Regex SimpleTimeOfTodayAfterRegex =
            new Regex(DateTimeDefinitions.SimpleTimeOfTodayAfterRegex, RegexFlags);

        public static readonly Regex SimpleTimeOfTodayBeforeRegex =
            new Regex(DateTimeDefinitions.SimpleTimeOfTodayBeforeRegex, RegexFlags);

        public static readonly Regex SpecificEndOfRegex =
            new Regex(DateTimeDefinitions.SpecificEndOfRegex, RegexFlags);

        public static readonly Regex UnspecificEndOfRegex =
            new Regex(DateTimeDefinitions.UnspecificEndOfRegex, RegexFlags);

        public static readonly Regex UnitRegex =
            new Regex(DateTimeDefinitions.TimeUnitRegex, RegexFlags);

        public static readonly Regex ConnectorRegex =
            new Regex(DateTimeDefinitions.ConnectorRegex, RegexFlags);

        public static readonly Regex NumberAsTimeRegex =
            new Regex(DateTimeDefinitions.NumberAsTimeRegex, RegexFlags);

        public static readonly Regex DateNumberConnectorRegex =
            new Regex(DateTimeDefinitions.DateNumberConnectorRegex, RegexFlags);

        public static readonly Regex YearRegex =
            new Regex(DateTimeDefinitions.YearRegex, RegexFlags);

        public static readonly Regex YearSuffix =
            new Regex(DateTimeDefinitions.YearSuffix, RegexFlags);

        public static readonly Regex SuffixAfterRegex =
            new Regex(DateTimeDefinitions.SuffixAfterRegex, RegexFlags);

        private const RegexOptions RegexFlags = RegexOptions.Singleline | RegexOptions.ExplicitCapture;

        public FrenchDateTimeExtractorConfiguration(IDateTimeOptionsConfiguration config)
            : base(config)
        {

            var numOptions = NumberOptions.None;
            if ((config.Options & DateTimeOptions.NoProtoCache) != 0)
            {
                numOptions = NumberOptions.NoProtoCache;
            }

            var numConfig = new BaseNumberOptionsConfiguration(config.Culture, numOptions);

            IntegerExtractor = Number.French.IntegerExtractor.GetInstance(numConfig);

            DatePointExtractor = new BaseDateExtractor(new FrenchDateExtractorConfiguration(this));
            TimePointExtractor = new BaseTimeExtractor(new FrenchTimeExtractorConfiguration(this));
            DurationExtractor = new BaseDurationExtractor(new FrenchDurationExtractorConfiguration(this));
            UtilityConfiguration = new FrenchDatetimeUtilityConfiguration();
            HolidayExtractor = new BaseHolidayExtractor(new FrenchHolidayExtractorConfiguration(this));

        }

        public IExtractor IntegerExtractor { get; }

        public IDateExtractor DatePointExtractor { get; }

        public IDateTimeExtractor TimePointExtractor { get; }

        public IDateTimeUtilityConfiguration UtilityConfiguration { get; }

        public IDateTimeExtractor HolidayExtractor { get; }

        Regex IDateTimeExtractorConfiguration.NowRegex => NowRegex;

        Regex IDateTimeExtractorConfiguration.SuffixRegex => SuffixRegex;

        Regex IDateTimeExtractorConfiguration.TimeOfTodayAfterRegex => TimeOfTodayAfterRegex;

        Regex IDateTimeExtractorConfiguration.SimpleTimeOfTodayAfterRegex => SimpleTimeOfTodayAfterRegex;

        Regex IDateTimeExtractorConfiguration.TimeOfTodayBeforeRegex => TimeOfTodayBeforeRegex;

        Regex IDateTimeExtractorConfiguration.SimpleTimeOfTodayBeforeRegex => SimpleTimeOfTodayBeforeRegex;

        Regex IDateTimeExtractorConfiguration.TimeOfDayRegex => TimeOfDayRegex;

        Regex IDateTimeExtractorConfiguration.SpecificEndOfRegex => SpecificEndOfRegex;

        Regex IDateTimeExtractorConfiguration.UnspecificEndOfRegex => UnspecificEndOfRegex;

        Regex IDateTimeExtractorConfiguration.UnitRegex => UnitRegex;

        Regex IDateTimeExtractorConfiguration.NumberAsTimeRegex => NumberAsTimeRegex;

        Regex IDateTimeExtractorConfiguration.DateNumberConnectorRegex => DateNumberConnectorRegex;

        Regex IDateTimeExtractorConfiguration.YearRegex => YearRegex;

        Regex IDateTimeExtractorConfiguration.YearSuffix => YearSuffix;

        Regex IDateTimeExtractorConfiguration.SuffixAfterRegex => SuffixAfterRegex;

        public IDateTimeExtractor DurationExtractor { get; }

        public bool IsConnector(string text)
        {
            return string.IsNullOrEmpty(text) || PrepositionRegex.IsMatch(text) || ConnectorRegex.IsMatch(text);
        }
    }
}
