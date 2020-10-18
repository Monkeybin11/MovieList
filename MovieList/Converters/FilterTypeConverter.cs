using System;
using System.Collections.Generic;
using System.Linq;

using MovieList.Core;
using MovieList.Core.ViewModels.Filters;
using MovieList.Properties;

using ReactiveUI;

namespace MovieList.Converters
{
    public sealed class FilterTypeConverter : IBindingTypeConverter, IEnumConverter<FilterType>
    {
        private readonly Dictionary<FilterType, string> filterTypeToString;
        private readonly Dictionary<string, FilterType> stringToFilterType;

        public FilterTypeConverter()
        {
            this.filterTypeToString = new Dictionary<FilterType, string>
            {
                [FilterType.Title] = Messages.FilterTypeTitle,
                [FilterType.Year] = Messages.FilterTypeYear,
                [FilterType.Kind] = Messages.FilterTypeKind,
                [FilterType.Tags] = Messages.FilterTypeTags,
                [FilterType.Standalone] = Messages.FilterTypeStandalone,
                [FilterType.Movie] = Messages.FilterTypeMovie,
                [FilterType.Series] = Messages.FilterTypeSeries,
                [FilterType.MovieWatched] = Messages.FilterTypeMovieWatched,
                [FilterType.MovieReleased] = Messages.FilterTypeMovieReleased,
                [FilterType.SeriesWatchStatus] = Messages.FilterTypeSeriesWatchStatus,
                [FilterType.SeriesReleaseStatus] = Messages.FilterTypeSeriesReleaseStatus,
                [FilterType.SeriesChannel] = Messages.FilterTypeSeriesChannel,
                [FilterType.SeriesNumberOfSeasons] = Messages.FilterTypeSeriesNumberOfSeasons,
                [FilterType.SeriesNumberOfEpisodes] = Messages.FilterTypeSeriesNumberOfEpisodes,
                [FilterType.SeriesMiniseries] = Messages.FilterTypeSeriesMiniseries,
                [FilterType.SeriesAnthology] = Messages.FilterTypeSeriesAnthology
            };

            this.stringToFilterType = filterTypeToString.ToDictionary(e => e.Value, e => e.Key);
        }

        public int GetAffinityForObjects(Type fromType, Type toType)
            => fromType == typeof(FilterType) || toType == typeof(FilterType)
                ? 10000
                : 0;

        public bool TryConvert(object? from, Type toType, object? conversionHint, out object? result)
        {
            switch (from)
            {
                case FilterType filterType:
                    result = this.filterTypeToString[filterType];
                    return true;
                case string str:
                    result = this.stringToFilterType[str];
                    return true;
                default:
                    result = null;
                    return false;
            }
        }

        public string ToString(FilterType filterType)
            => this.filterTypeToString[filterType];

        public FilterType FromString(string str)
            => this.stringToFilterType.ContainsKey(str)
                ? this.stringToFilterType[str]
                : throw new ArgumentOutOfRangeException(nameof(str));
    }
}