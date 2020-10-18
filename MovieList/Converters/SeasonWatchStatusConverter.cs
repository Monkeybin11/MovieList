using System;
using System.Collections.Generic;
using System.Linq;

using MovieList.Core;
using MovieList.Data.Models;
using MovieList.Properties;

using ReactiveUI;

namespace MovieList.Converters
{
    public sealed class SeasonWatchStatusConverter : IBindingTypeConverter, IEnumConverter<SeasonWatchStatus>
    {
        private readonly Dictionary<SeasonWatchStatus, string> statusToString;
        private readonly Dictionary<string, SeasonWatchStatus> stringToStatus;

        public SeasonWatchStatusConverter()
        {
            this.statusToString = new Dictionary<SeasonWatchStatus, string>
            {
                [SeasonWatchStatus.NotWatched] = Messages.SeasonNotWatched,
                [SeasonWatchStatus.Watching] = Messages.SeasonWatching,
                [SeasonWatchStatus.Hiatus] = Messages.SeasonHiatus,
                [SeasonWatchStatus.Watched] = Messages.SeasonWatched,
                [SeasonWatchStatus.StoppedWatching] = Messages.SeasonStoppedWatching
            };

            this.stringToStatus = statusToString.ToDictionary(e => e.Value, e => e.Key);
        }

        public int GetAffinityForObjects(Type fromType, Type toType)
            => fromType == typeof(SeasonWatchStatus) || toType == typeof(SeasonWatchStatus)
                ? 10000
                : 0;

        public bool TryConvert(object? from, Type toType, object? conversionHint, out object? result)
        {
            switch (from)
            {
                case SeasonWatchStatus status:
                    result = this.statusToString[status];
                    return true;
                case string str:
                    result = this.stringToStatus[str];
                    return true;
                default:
                    result = null;
                    return false;
            }
        }

        public string ToString(SeasonWatchStatus status)
            => this.statusToString[status];

        public SeasonWatchStatus FromString(string str)
            => this.stringToStatus.ContainsKey(str)
                ? this.stringToStatus[str]
                : throw new ArgumentOutOfRangeException(nameof(str));
    }
}
