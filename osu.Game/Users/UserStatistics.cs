﻿// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using Newtonsoft.Json;
using osu.Game.Scoring;
using osu.Game.Utils;
using static osu.Game.Users.User;

namespace osu.Game.Users
{
    public class UserStatistics
    {
        [JsonProperty]
        public User User;

        [JsonProperty(@"level")]
        public LevelInfo Level;

        public struct LevelInfo
        {
            [JsonProperty(@"current")]
            public int Current;

            [JsonProperty(@"progress")]
            public int Progress;
        }

        [JsonProperty(@"global_rank")]
        public int? GlobalRank;

        public int? CountryRank;

        [JsonProperty(@"rank")]
        private UserRanks ranks
        {
            // eventually that will also become an own json property instead of reading from a `rank` object.
            // see https://github.com/ppy/osu-web/blob/cb79bb72186c8f1a25f6a6f5ef315123decb4231/app/Transformers/UserStatisticsTransformer.php#L53.
            set => CountryRank = value.Country;
        }

        [JsonProperty(@"pp")]
        public decimal? PP;

        [JsonProperty(@"pp_rank")]
        public int PPRank;

        [JsonProperty(@"ranked_score")]
        public long RankedScore;

        [JsonProperty(@"hit_accuracy")]
        public decimal Accuracy;

        [JsonIgnore]
        public string DisplayAccuracy => Accuracy.FormatAccuracy();

        [JsonProperty(@"play_count")]
        public int PlayCount;

        [JsonProperty(@"play_time")]
        public int? PlayTime;

        [JsonProperty(@"total_score")]
        public long TotalScore;

        [JsonProperty(@"total_hits")]
        public int TotalHits;

        [JsonProperty(@"maximum_combo")]
        public int MaxCombo;

        [JsonProperty(@"replays_watched_by_others")]
        public int ReplaysWatched;

        [JsonProperty(@"grade_counts")]
        public Grades GradesCount;

        public struct Grades
        {
            [JsonProperty(@"ssh")]
            public int? SSPlus;

            [JsonProperty(@"ss")]
            public int SS;

            [JsonProperty(@"sh")]
            public int? SPlus;

            [JsonProperty(@"s")]
            public int S;

            [JsonProperty(@"a")]
            public int A;

            public int this[ScoreRank rank]
            {
                get
                {
                    switch (rank)
                    {
                        case ScoreRank.XH:
                            return SSPlus ?? 0;

                        case ScoreRank.X:
                            return SS;

                        case ScoreRank.SH:
                            return SPlus ?? 0;

                        case ScoreRank.S:
                            return S;

                        case ScoreRank.A:
                            return A;

                        default:
                            throw new ArgumentException($"API does not return {rank.ToString()}");
                    }
                }
            }
        }

        private struct UserRanks
        {
            [JsonProperty(@"country")]
            public int? Country;
        }

        public RankHistoryData RankHistory;
    }
}
