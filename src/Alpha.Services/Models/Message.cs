using System;
using System.Collections.Generic;
using Alpha.Services.Constants;

namespace Alpha.Services.Models
{
    public class Message
    {
        public Message(
            int id,
            string contentEN,
            string contentCS,
            string contentCH,
            string contentJP,
            string contentTH,
            string contentIT,
            string contentKO,
            string contentES,
            string contentVI,
            DateTime postTime,
            DateTime createTime,
            DateTime lastUpdateTime,
            byte messageType,
            bool isSticky,
            bool isTest,
            int matchId,
            int creator,
            int lastUpdator,
            string creatorName,
            string lastUpdateName,
            bool isBatch,
            string bettransId,
            int userId,
            string userName,
            long rowNum)
        {
            Id = id;
            PostTime = postTime;
            CreateTime = createTime;
            LastUpdateTime = lastUpdateTime;
            MessageType = messageType;
            IsSticky = isSticky;
            IsTest = isTest;
            MatchId = matchId;
            CreatorId = creator;
            LastUpdator = lastUpdator;
            CreatorName = creatorName;
            LastUpdateName = lastUpdateName;
            IsBatch = isBatch;
            BetTransId = bettransId;
            UserId = userId;
            UserName = userName;
            RowNum = rowNum;

            Contents = new Dictionary<string, string>
            {
                [LanguageConstants.English] = contentEN ?? string.Empty,
                [LanguageConstants.ChineseSimplified] = contentCS ?? string.Empty,
                [LanguageConstants.ChineseTraditional] = contentCH ?? string.Empty,
                [LanguageConstants.Japanese] = contentJP ?? string.Empty,
                [LanguageConstants.Thai] = contentTH ?? string.Empty,
                [LanguageConstants.Korean] = contentKO ?? string.Empty,
                [LanguageConstants.Vietnamese] = contentVI ?? string.Empty
            };
        }

        private readonly Dictionary<string, string> Contents;

        public int Id { get; private set; }

        public DateTime PostTime { get; private set; }

        public DateTime CreateTime { get; private set; }

        public DateTime LastUpdateTime { get; internal set; }

        public byte MessageType { get; internal set; }

        public bool IsSticky { get; internal set; }

        public bool IsTest { get; private set; }

        public int MatchId { get; private set; }

        public int CreatorId { get; private set; }

        public int LastUpdator { get; private set; }

        public string CreatorName { get; private set; }

        public string LastUpdateName { get; private set; }

        public bool IsBatch { get; private set; }

        public string BetTransId { get; private set; }

        public int UserId { get; private set; }

        public string UserName { get; private set; }

        public long RowNum { get; private set; }

        public string GetContentByKey(string languageKey)
            => Contents.ContainsKey(languageKey) && !string.IsNullOrEmpty(Contents[languageKey])
            ? Contents[languageKey]
            : Contents[LanguageConstants.English];
    }
}