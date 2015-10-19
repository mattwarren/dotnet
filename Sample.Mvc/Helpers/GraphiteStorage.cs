using StatsdClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StackExchange.Profiling.Storage
{
    /// <summary>
    /// Store the MiniProfiler results using Graphite (http://graphite.wikidot.com/)
    /// </summary>
    public class GraphiteStorage : IStorage
    {
        private static readonly List<Guid> EmptyList = new List<Guid>();
        private static readonly IEnumerable<Guid> EmptyEnumerable = Enumerable.Empty<Guid>();

        /// <summary>
        /// List the latest profiling results.
        /// </summary>
        public IEnumerable<Guid> List(
            int maxResults,
            DateTime? start = null,
            DateTime? finish = null,
            ListResultsOrder orderBy = ListResultsOrder.Descending)
        {
            // do nothing, i.e. return an empty result
            return EmptyEnumerable;
        }

        /// <summary>
        /// Stores <paramref name="profiler"/> under its <see cref="MiniProfiler.Id"/>.
        /// </summary>
        /// <param name="profiler">The results of a profiling session.</param>
        /// <remarks>
        /// Should also ensure the profiler is stored as being un-viewed by its profiling <see cref="MiniProfiler.User"/>.
        /// </remarks>
        public void Save(MiniProfiler profiler)
        {
            // ignore browerLink URL, i.e. "/__browserLink/requestData/44067061dcd44ffcbbca1da"
            if (profiler.Name == null || profiler.Name.Contains("__browserLink"))
                return;

            // Convert the path into something that plays better with Graphite, i.e. "Home/MinSaveMs" -> "Home.MinSaveMs"
            Metrics.Timer(
                    profiler.Name.Replace("/", "."),
                    (int)profiler.DurationMilliseconds);
        }

        /// <summary>
        /// Returns a <see cref="MiniProfiler"/> from storage based on <paramref name="id"/>,
        /// which should map to <see cref="MiniProfiler.Id"/>.
        /// </summary>
        /// <remarks>
        /// Should also update that the resulting profiler has been marked as viewed by its
        /// profiling <see cref="MiniProfiler.User"/>.
        /// </remarks>
        public MiniProfiler Load(Guid id)
        {
            // do nothing, i.e. return an empty result
            return null;
        }
        /// <summary>
        /// Sets a particular profiler session so it is considered "un-viewed"
        /// </summary>
        public void SetUnviewed(string user, Guid id)
        {
            // do nothing
        }

        /// <summary>
        /// Sets a particular profiler session to "viewed"
        /// </summary>
        public void SetViewed(string user, Guid id)
        {
            // do nothing
        }

        /// <summary>
        /// Returns a list of <see cref="MiniProfiler.Id"/>s that haven't been seen by <paramref name="user"/>.
        /// </summary>
        /// <param name="user">
        /// User identified by the current <c>MiniProfiler.Settings.UserProvider</c>
        /// </param>
        public List<Guid> GetUnviewedIds(string user)
        {
            // do nothing, i.e. return an empty result
            return EmptyList;
        }
    }
}
