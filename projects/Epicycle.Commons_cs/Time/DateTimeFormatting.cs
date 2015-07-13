// [[[[INFO>
// Copyright 2015 Epicycle (http://epicycle.org, https://github.com/open-epicycle)
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
// For more information check https://github.com/open-epicycle/Epicycle.Commons-cs
// ]]]]

using System;

namespace Epicycle.Commons.Time
{
    public static class DateTimeFormatting
    {
        public const UtcAndLocalTemplate DefaultUtcAndLocalTemplate = UtcAndLocalTemplate.Utc;
        public const ISO8601Template DefaultISO8601Template = ISO8601Template.DateTime;
        public const ISO8601Format DefaultISO8601Format = ISO8601Format.Pretty;

        public enum UtcAndLocalTemplate
        {
            Utc,
            Local,
            UtcAndLocal,
            LocalAndUtc,
        }

        public enum ISO8601Template
        {
            DateOnly,
            TimeOnly,
            DateTime,
        }

        public enum ISO8601Format
        {
            Pretty,
            Formal,
            FileSystemPathFriendly,
        }

        public static string ToStringISO8601(this DateTime @this, ISO8601Template template = DefaultISO8601Template, ISO8601Format format = DefaultISO8601Format)
        {
            var suffix = @this.Kind == DateTimeKind.Utc ? "Z" : "";

            var dateFormatString = "yyyy-MM-dd";
            var timeFormatString = format != ISO8601Format.FileSystemPathFriendly ? "HH\\:mm\\:ss" : "HH\\_mm\\_ss";
            var dateTimeSeparator = format == ISO8601Format.Pretty ? ' ' : 'T';

            switch (template)
            {
                case ISO8601Template.DateOnly:
                    return @this.ToString(dateFormatString + suffix);
                case ISO8601Template.TimeOnly:
                    return @this.ToString(timeFormatString + suffix);
                case ISO8601Template.DateTime:
                    return @this.ToString(string.Format("{0}{1}{2}{3}", dateFormatString, dateTimeSeparator, timeFormatString, suffix));
                default:
                    throw new ArgumentException("Illegal ISO8601Template");
            }
        }

        public static string ToStringISO8601(
            this DateTimeUtcAndLocal @this,
            UtcAndLocalTemplate template = DefaultUtcAndLocalTemplate, 
            ISO8601Template dateTemplate = DefaultISO8601Template,
            ISO8601Format format = DefaultISO8601Format)
        {
            var utcString = template != UtcAndLocalTemplate.Local ? @this.Utc.ToStringISO8601(dateTemplate, format) : null;
            var localString = template != UtcAndLocalTemplate.Utc ? @this.Local.ToStringISO8601(dateTemplate, format) : null;

            string twoDatesFormatString;
            switch(format)
            {
                case ISO8601Format.Pretty:
                    twoDatesFormatString = "{0} / {1}";
                    break;
                case ISO8601Format.Formal:
                    twoDatesFormatString = "{0} {1}";
                    break;
                case ISO8601Format.FileSystemPathFriendly:
                    twoDatesFormatString = "{0}--{1}";
                    break;
                default:
                    throw new ArgumentException("Illegal ISO8601Format");
            }

            switch(template)
            {
                case UtcAndLocalTemplate.Utc:
                    return utcString;
                case UtcAndLocalTemplate.Local:
                    return localString;
                case UtcAndLocalTemplate.UtcAndLocal:
                    return string.Format(twoDatesFormatString, utcString, localString);
                case UtcAndLocalTemplate.LocalAndUtc:
                    return string.Format(twoDatesFormatString, localString, utcString);
                default:
                    throw new ArgumentException("Illegal UtcAndLocalTemplate");
            }
        }
    }
}
