using System.Collections.Generic;

using MovieList.Data.Models;
using MovieList.Properties;

namespace MovieList.Converters
{
    public sealed class SeriesReleaseStatusConverter : EnumConverter<SeriesReleaseStatus>
    {
        protected override Dictionary<SeriesReleaseStatus, string> CreateConverterDictionary() =>
            new()
            {
                [SeriesReleaseStatus.NotStarted] = Messages.SeriesNotStarted,
                [SeriesReleaseStatus.Running] = Messages.SeriesRunning,
                [SeriesReleaseStatus.Finished] = Messages.SeriesFinished,
                [SeriesReleaseStatus.Cancelled] = Messages.SeriesCancelled,
                [SeriesReleaseStatus.Unknown] = Messages.SeriesUnknown
            };
    }
}
